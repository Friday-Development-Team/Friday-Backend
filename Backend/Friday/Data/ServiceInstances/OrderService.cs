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

            return new OrderHistory
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

        }
        /// <inheritdoc />
        public async Task<bool> SetAccepted(int id, bool value, bool toKitchen)
        {

            var needsKitchen = await context.Configuration.SingleAsync();

            var changed = value ? (toKitchen && !needsKitchen.CombinedCateringKitchen ? OrderStatus.SentToKitchen : OrderStatus.Accepted) : OrderStatus.Pending;
            var item = orders.SingleOrDefault(s => s.Id == id);

            if (item == null || item.StatusBeverage == OrderStatus.Completed || item.StatusFood == OrderStatus.Completed || !item.IsOngoing() ||
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
            if (orderdto == null || !orderdto.IsValid())
                throw new ArgumentException();

            var user = await users.SingleAsync(s => s.Name == username);

            var order = new Order
            {
                User = user,
                UserId = user.Id,
                OrderTime = DateTime.Now
            };

            var orderitems = orderdto.Items.Select(s =>
                    new OrderItem
                    {
                        Order = order,
                        Item = items.SingleOrDefault(t => t.Id == s.Id),
                        Amount = s.Amount
                    })
                .ToList();
            var totalPrice = orderitems.Select(s => (s.Amount * s.Item.Price)).Sum();

            if (!user.HasBalance(totalPrice))
                return 0;

            var hasBev = orderitems.Any(s => s.Item.Type == ItemType.Beverage);
            var hasFood = orderitems.Any(s => s.Item.Type == ItemType.Food);

            order.StatusBeverage = (hasBev ? OrderStatus.Pending : OrderStatus.None);
            order.StatusFood = (hasFood ? OrderStatus.Pending : OrderStatus.None);

            order.Items = orderitems;
            // IList<Item> rem = new List<Item>();
            var log = new Dictionary<Item, int>();
            foreach (var item in orderdto.Items)
            {
                var temp = await items.SingleAsync(s => s.Id == item.Id);

                //temp.Amount -= item.Amount;
                if (await itemService.ChangeCount(user, temp.Id, -item.Amount)) //Should it fail, the item is rejected. This will be notified to the catering. This will not affect the Users balance.
                    log.Add(temp, -item.Amount);
                else
                    RevertUser(user, temp.Count * temp.Price);//Refunds the failed item. It will not show up in the history.


                items.Update(temp);//Updated Item
            }

            var result = await orders.AddAsync(order);//Order added
            //user.UpdateBalance(-totalPrice);

            //users.Update(user);//Updated user

            await context.SaveChangesAsync();

            await userService.ChangeBalance(user.Name, -totalPrice, true);

            return result.Entity.Id;
        }


        private async void RevertUser(ShopUser user, double amount)
        {
            await userService.ChangeBalance(user.Id, amount, false);
            user.UpdateBalance(amount);
        }

        /// <inheritdoc />
        public async Task<bool> SetCompleted(int id, bool forBeverage)
        {
            var order = await orders.SingleAsync(s => s.Id == id);
            if ((forBeverage ? order.StatusBeverage != OrderStatus.Accepted : order.StatusFood != OrderStatus.Accepted))//Only accepted orders can be completed. SentToKitchen has to return them to Catering.
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
            var order =await orders.SingleAsync(s => s.Id == id);
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
                .Where(s => (isKitchen ? s.StatusFood == OrderStatus.SentToKitchen : s.IsOngoing()))
                .OrderBy(s => (int)s.StatusBeverage).ThenBy(s => (int)s.StatusFood).ThenBy(s => s.OrderTime).AsNoTracking()
                .Select(s => new CateringOrder
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
    }
}
