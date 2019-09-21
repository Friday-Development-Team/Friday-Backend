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

namespace Friday.Data.ServiceInstances {
    public class OrderService : IOrderService {

        private readonly Context context;
        private readonly DbSet<Order> orders;
        private readonly DbSet<ShopUser> users;
        private readonly DbSet<Item> items;

        public OrderService(Context context) {
            this.context = context;
            orders = context.Orders;
            users = context.Users;
            items = context.Items;
        }
        /// <inheritdoc />
        public OrderHistory GetHistory(string username) {
            if (users.Single(s => s.Name == username) == null)
                return null;

            return new OrderHistory {
                UserName = username,
                orders = orders.Where(s => s.User.Name == username).Where(s => s.Status == OrderStatus.Completed)
                    .OrderBy(s => s.OrderTime)
                    .Select(s =>
                        new HistoryOrder {
                            OrderTime = s.OrderTime,
                            CompletionTime = s.CompletionTime,
                            TotalPrice = s.Items.Select(t => t.Item.Count * t.Item.Price).Sum(),
                            Items = s.Items.Select(t => new HistoryOrderItem { ItemName = t.Item.Name, Amount = t.Amount }).ToList()
                        })
                    .ToList()
            };

        }
        /// <inheritdoc />
        public bool SetAccepted(int id, bool value) {
            var changed = value ? OrderStatus.Accepted : OrderStatus.Pending;
            var item = orders.SingleOrDefault(s => s.Id == id);

            if (item == null || item.Status == OrderStatus.Completed)
                return false;

            var old = item.Status;
            item.Status = changed;
            orders.Update(item);
            return item.Status == changed && item.Status != old;//Ensures the value was correctly set. Returns false if it was already the given value.
        }
        /// <inheritdoc />
        public bool PlaceOrder(OrderDTO orderdto) {
            if (orderdto == null || !orderdto.IsValid())
                return false;

            var user = users.SingleOrDefault(s => s.Name == orderdto.Username);
            if (user == null)
                return false;

            Order order = new Order {
                User = user,
                UserId = user.Id,
                OrderTime = DateTime.Now,
                Status = OrderStatus.Pending
            };

            var orderitems = orderdto.Items.Select(s =>
                    new OrderItem {
                        Order = order,
                        Item = items.SingleOrDefault(t => t.Id == s.Id),
                        Amount = s.Amount
                    })
                .ToList();
            var totalPrice = orderitems.Select(s => (s.Amount * s.Item.Price)).Sum();

            if (!user.HasBalance(totalPrice))
                return false;

            order.Items = orderitems;


            foreach (var item in order.Items) {
                var temp = items.SingleOrDefault(s => s.Id == item.Item.Id);
                if (temp == null)
                    return false;

                temp.Count -= item.Amount;

                items.Update(temp);//Updated Item
            }

            orders.Add(order);//Order added
            user.UpdateBalance(-totalPrice);

            users.Update(user);//Updated user

            context.SaveChanges();

            return true;
        }

        /// <inheritdoc />
        public bool SetCompleted(int id) {
            var order = orders.SingleOrDefault(s => s.Id == id);
            if (order == null || order.Status != OrderStatus.Accepted)//Only accepted orders can be completed
                return false;
            order.Status = OrderStatus.Completed;
            orders.Update(order);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc />
        public bool Cancel(int id) {//#TODO Config for option to allow accepted orders to be cancelled
            var order = orders.SingleOrDefault(s => s.Id == id);
            if (order == null || order.Status != OrderStatus.Pending)
                return false;
            order.Status = OrderStatus.Cancelled;
            orders.Update(order);
            context.SaveChanges();
            return true;
        }
        /// <inheritdoc />
        public string GetStatus(int id) {
            return orders.SingleOrDefault(s => s.Id == id)?.ToString();
        }
        /// <inheritdoc />
        public IList<Order> GetAll() {
            return orders.Where(s => s.IsOngoing()).OrderBy(s => (int)s.Status).ThenBy(s => s.OrderTime).AsNoTracking().ToList();
        }
    }
}
