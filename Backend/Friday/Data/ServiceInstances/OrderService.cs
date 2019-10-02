using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances
{
    public class OrderService : IOrderService
    {

        private readonly Context context;
        private readonly DbSet<Order> orders;
        private readonly DbSet<ShopUser> users;
        private readonly DbSet<Item> items;

        private readonly IItemService itemService;
        private readonly IUserService userService;

        public OrderService(Context context, IItemService itemsService, IUserService userService)
        {
            this.context = context;
            orders = context.Orders;
            users = context.ShopUsers;
            items = context.Items;
            this.itemService = itemsService;
            this.userService = userService;
        }
        /// <inheritdoc />
        public OrderHistory GetHistory(string username)
        {
            if (users.Single(s => s.Name == username) == null)
                return null;

            return new OrderHistory
            {
                UserName = username,
                orders = orders.Include(s => s.Items).Include(s => s.User).Where(s => s.User.Name == username).Where(s => s.Status == OrderStatus.Completed)
                    .OrderBy(s => s.OrderTime)
                    .Select(s =>
                        new HistoryOrder
                        {
                            OrderTime = s.OrderTime,
                            CompletionTime = s.CompletionTime,
                            TotalPrice = s.Items.Select(t => t.Amount * t.Item.Price).Sum(),
                            Items = s.Items.Select(t => new HistoryOrderItem { ItemName = t.Item.Name, Amount = t.Amount }).ToList()
                        })
                    .ToList()
            };

        }
        /// <inheritdoc />
        public bool SetAccepted(int id, bool value, bool toKitchen)
        {

            var needsKitchen = context.Configuration.Single();

            var changed = value ? (toKitchen && !needsKitchen.CombinedCateringKitchen ? OrderStatus.SentToKitchen : OrderStatus.Accepted) : OrderStatus.Pending;
            var item = orders.SingleOrDefault(s => s.Id == id);

            if (item == null || item.Status == OrderStatus.Completed || !item.IsOngoing())
                return false;

            var old = item.Status;
            item.Status = changed;
            orders.Update(item);
            context.SaveChanges();
            return item.Status == changed && item.Status != old;//Ensures the value was correctly set. Returns false if it was already the given value.
        }
        /// <inheritdoc />
        public int PlaceOrder(string username, OrderDTO orderdto)
        {
            if (orderdto == null || !orderdto.IsValid())
                return 0;

            var user = users.SingleOrDefault(s => s.Name == username);
            if (user == null)
                return 0;

            Order order = new Order
            {
                User = user,
                UserId = user.Id,
                OrderTime = DateTime.Now,
                Status = OrderStatus.Pending
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

            order.Items = orderitems;

            Dictionary<Item, int> log = new Dictionary<Item, int>();
            foreach (var item in order.Items)
            {
                var temp = items.SingleOrDefault(s => s.Id == item.Item.Id);
                if (temp == null)
                    return 0;

                //temp.Count -= item.Amount;
                if (itemService.ChangeCount(temp.Id, -temp.Count))
                    RevertItems(log);
                log.Add(temp, temp.Count);


                items.Update(temp);//Updated Item
            }

            var result = orders.Add(order);//Order added
            user.UpdateBalance(-totalPrice);

            users.Update(user);//Updated user

            context.SaveChanges();

            return result.Entity.Id;
        }

        private void RevertItems(Dictionary<Item, int> log)
        {
            foreach (var entry in log)
            {
                var item = entry.Key;
                var amount = entry.Value;
                itemService.ChangeCount(item.Id, amount);
            }
        }

        private void RevertUser(int id, double amount)
        {

        }

        /// <inheritdoc />
        public bool SetCompleted(int id)
        {
            var order = orders.SingleOrDefault(s => s.Id == id);
            if (order == null || order.Status != OrderStatus.Accepted)//Only accepted orders can be completed
                return false;
            order.Status = OrderStatus.Completed;
            orders.Update(order);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc />
        public bool Cancel(int id)
        {//#TODO Config for option to allow accepted orders to be cancelled
            var order = orders.SingleOrDefault(s => s.Id == id);
            if (order == null || !order.CanBeCancelled(context.Configuration.Single().CancelOnAccepted))
                return false;
            order.Status = OrderStatus.Cancelled;
            orders.Update(order);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc />
        public string GetStatus(int id)
        {
            return orders.SingleOrDefault(s => s.Id == id)?.Status.ToString();
        }
        /// <inheritdoc />
        public IList<CateringOrderDTO> GetAll(bool isKitchen)
        {
            var result = orders.Include(s => s.Items).ThenInclude(s => s.Item).ThenInclude(s => s.ItemDetails)
                .Include(s => s.User)
                .Where(s => (isKitchen ? s.Status == OrderStatus.SentToKitchen : s.IsOngoing()))
                .OrderBy(s => (int)s.Status).ThenBy(s => s.OrderTime).AsNoTracking()
                .Select(s => new CateringOrderDTO
                {
                    Id = s.Id,
                    Status = ((OrderStatus)s.Status).ToString(),
                    User = s.User,
                    Items = s.Items.Select(t => new HistoryOrderItem { Amount = t.Amount, ItemName = t.Item.Name })
                        .ToList()
                })
                .ToList();

            return result;
        }
    }
}
