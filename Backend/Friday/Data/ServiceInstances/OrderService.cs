using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Data.ServiceInstances
{
    /// <inheritdoc cref="IOrderService" />
    public class OrderService : ServiceBase, IOrderService
    {

        private readonly DbSet<Order> orders;
        private readonly DbSet<ShopUser> users;
        private readonly DbSet<Item> items;

        private readonly IItemService itemService;
        private readonly IUserService userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="itemsService"></param>
        /// <param name="userService"></param>
        public OrderService(Context context, IItemService itemsService, IUserService userService) : base(context)
        {
            orders = context.Orders;
            users = context.ShopUsers;
            items = context.Items;
            this.itemService = itemsService;
            this.userService = userService;
        }
        /// <inheritdoc />
        public async Task<OrderHistory> GetHistory(string username)
        {
            await users.SingleAsync(s => s.Name == username); //Checks if the user exists

            var history = new OrderHistory
            {
                UserName = username,
                Orders = await orders.Include(s => s.Items).Include(s => s.User)
                    .Where(s => s.User.Name == username)
                    .Where(s => (s.StatusBeverage == OrderStatus.Completed || s.StatusBeverage == OrderStatus.None) &&
                                (s.StatusFood == OrderStatus.Completed || s.StatusFood == OrderStatus.None))
                    .OrderBy(s => s.OrderTime)
                    .Select(s =>
                        new HistoryOrder
                        {
                            OrderTime = s.OrderTime,
                            CompletionTimeBeverage = s.CompletionTimeBeverage,
                            CompletionTimeFood = s.CompletionTimeFood,
                            TotalPrice = s.Items.Select(t => t.Amount * t.Item.Price).Sum(),
                            Items = s.Items.Select(t => new HistoryOrderItem { ItemName = t.Item.Name, Amount = t.Amount }).ToList()
                        })
                    .ToListAsync(),

            };

            return history;

        }
        /// <inheritdoc />
        public async Task<bool> SetAccepted(int id, bool value, bool toKitchen)
        {

            var needsKitchen = await context.Configuration.SingleAsync();// Get config options first

            // Sets the new value based on the config
            var changed = value ? (toKitchen && !needsKitchen.CombinedCateringKitchen ? OrderStatus.SentToKitchen : OrderStatus.Accepted) : OrderStatus.Pending;

            var item = await orders.SingleAsync(s => s.Id == id);

            // Throw out if invalid status
            if (item.StatusBeverage == OrderStatus.Completed || item.StatusFood == OrderStatus.Completed || !item.IsOngoing() ||
                (item.StatusFood == OrderStatus.None && toKitchen))
                return false;



            if (item.StatusFood != OrderStatus.None)
                item.StatusFood = changed;
            if (item.StatusBeverage != OrderStatus.None)
                item.StatusBeverage = value ? OrderStatus.Accepted : OrderStatus.Pending;

            orders.Update(item);
            await context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<int> PlaceOrder(string username, OrderDTO orderdto)
        {
            // Throw out invalid data first
            if (orderdto == null || !orderdto.IsValid())
                throw new ArgumentException("Order could not be found");

            var user = await users.SingleAsync(s => s.Name == username);

            // Create new order object
            var order = new Order
            {
                User = user,
                OrderTime = DateTime.Now
            };

            // Create list of order items from dto 
            var orderitems = orderdto.Items.Select(s =>
                    new OrderItem
                    {
                        Order = order,
                        Item = items.SingleOrDefault(t => t.Id == s.Id),
                        Amount = s.Amount
                    })
                .ToList();

            // And get total price
            var totalPrice = orderitems.Select(s => (s.Amount * s.Item.Price)).Sum();

            // Balance check
            if (!user.HasBalance(totalPrice))
                return 0;


            var hasBev = orderitems.Any(s => s.Item.Type == ItemType.Beverage);
            var hasFood = orderitems.Any(s => s.Item.Type == ItemType.Food);

            // Set status to none if none are present
            order.StatusBeverage = (hasBev ? OrderStatus.Pending : OrderStatus.None);
            order.StatusFood = (hasFood ? OrderStatus.Pending : OrderStatus.None);


            order.Items = orderitems;
            var log = new Dictionary<Item, int>();

            // Go over all items and change their count
            foreach (var item in orderdto.Items)
            {
                var dbItem = await items.SingleAsync(s => s.Id == item.Id);

                try
                {
                    //temp.Amount -= item.Amount;
                    if (await itemService.ChangeCount(user, dbItem.Id, -item.Amount)) //Should it fail, the item is rejected. This will be notified to the catering. This will not affect the Users balance.
                        log.Add(dbItem, -item.Amount);
                    else
                        RevertUser(user, dbItem.Count * dbItem.Price);//Refunds the failed item. It will not show up in the history.
                }
                catch (Exception)
                {
                    throw;
                }

                items.Update(dbItem);//Updated Item
            }

            var result = await orders.AddAsync(order);//Order added

            await context.SaveChangesAsync();

            await userService.ChangeBalance(user.Name, -totalPrice, true);

            return result.Entity.Id;
        }


        private async void RevertUser(ShopUser user, double amount)
        {
            await userService.ChangeBalance(user.Id, amount, false);// Change it back
            user.UpdateBalance(amount);
        }

        /// <inheritdoc />
        public async Task<bool> SetCompleted(int id, bool forBeverage)
        {

            var order = await orders.SingleAsync(s => s.Id == id);

            //Only accepted orders can be completed. SentToKitchen has to return them to Catering.
            if ((forBeverage ? order.StatusBeverage != OrderStatus.Accepted : order.StatusFood != OrderStatus.Accepted))
                return false;

            if (forBeverage)
            {
                order.StatusBeverage = OrderStatus.Completed;
                order.CompletionTimeBeverage = DateTime.Now;
            }
            else
            {
                order.StatusFood = OrderStatus.Completed;
                order.CompletionTimeFood = DateTime.Now;
            }

            orders.Update(order);
            await context.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> Cancel(int id)
        {
            var order = await orders.SingleAsync(s => s.Id == id);

            // Check config first
            if (!order.CanBeCancelled((await context.Configuration.SingleAsync()).CancelOnAccepted))
                return false;

            order.StatusBeverage = OrderStatus.Cancelled;
            order.StatusFood = OrderStatus.Cancelled;

            orders.Update(order);
            await context.SaveChangesAsync();
            return true;
        }
        /// <inheritdoc />
        public async Task<string> GetStatus(int id)
        {
            var order = await orders.SingleAsync(s => s.Id == id);
            return order.StatusBeverage + order.StatusFood.ToString();
        }
        /// <inheritdoc />
        public async Task<IList<CateringOrder>> GetAll(bool isKitchen)
        {
            return await orders
                .Include(s => s.Items)
                .ThenInclude(s => s.Item)
                .ThenInclude(s => s.ItemDetails)
                .Include(s => s.User)
                .AsNoTracking()
                .Where(s => (isKitchen ? s.StatusFood == OrderStatus.SentToKitchen : s.StatusBeverage == OrderStatus.Pending || s.StatusBeverage == OrderStatus.Accepted ||
                    s.StatusFood == OrderStatus.SentToKitchen || s.StatusFood == OrderStatus.Pending ||
                    s.StatusFood == OrderStatus.Accepted))// 
                .OrderBy(s => (int)s.StatusBeverage).ThenBy(s => (int)s.StatusFood).ThenBy(s => s.OrderTime)
                .Select(s => new CateringOrder // Put into DTO output object
                {
                    Id = s.Id,
                    StatusBeverage = isKitchen ? OrderStatus.None : s.StatusBeverage,
                    StatusFood = s.StatusFood,
                    User = s.User.Name,
                    Items = s.Items.Select(t => new HistoryOrderItem { Amount = t.Amount, ItemName = t.Item.Name })
                        .ToList(),
                    OrderTime = s.OrderTime,
                    TotalPrice = s.Items.Sum(t => t.Amount * t.Item.Price)
                })
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<IList<CateringOrder>> GetRunningOrders(string username)
        {
            return await users.Include(s => s.Orders).ThenInclude(s => s.Items).AsNoTracking()
                .Where(s => s.Name == username)
                .SelectMany(s => s.Orders)
                // Check status for running only
                .Where(s => s.StatusBeverage == OrderStatus.Pending || s.StatusBeverage == OrderStatus.Accepted ||
                                                      s.StatusFood == OrderStatus.SentToKitchen || s.StatusFood == OrderStatus.Pending ||
                                                      s.StatusFood == OrderStatus.Accepted)
                .OrderBy(s => (int)s.StatusBeverage).ThenBy(s => (int)s.StatusFood).ThenBy(s => s.OrderTime)
                .Select(s => new CateringOrder
                {
                    Id = s.Id,
                    StatusBeverage = s.StatusBeverage,
                    StatusFood = s.StatusFood,
                    User = s.User.Name,
                    Items = s.Items.Select(t => new HistoryOrderItem { Amount = t.Amount, ItemName = t.Item.Name })
                        .ToList(),
                    OrderTime = s.OrderTime,
                    TotalPrice = s.Items.Sum(t => t.Amount * t.Item.Price)
                })
                .ToListAsync();
        }

        public async Task<IList<HistoryOrder>> GetTotalHistory()
        {
            return await orders.Include(s => s.Items).ThenInclude(s => s.Item).Include(s => s.User).AsNoTracking()
                .Where(s => (s.StatusBeverage == OrderStatus.Completed || s.StatusBeverage == OrderStatus.None) &&
                            (s.StatusFood == OrderStatus.Completed || s.StatusFood == OrderStatus.None))
                .OrderByDescending(s => s.Id)
                .Select(s => new HistoryOrder
                {
                    CompletionTimeFood = s.CompletionTimeFood,
                    CompletionTimeBeverage = s.CompletionTimeBeverage,
                    OrderTime = s.OrderTime,
                    TotalPrice = s.Items.Select(t => (t.Item.Price * t.Amount)).Sum(),
                    Items = s.Items.Select(t => new HistoryOrderItem {Amount = t.Amount, ItemName = t.Item.Name})
                        .ToList()
                }).ToListAsync();

        }
    }
}
