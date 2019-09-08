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
            orders = context.orders;
            users = context.users;
            items = context.items;
        }
        /// <summary>
        /// Returns all the completed Orders of a specified user.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>Object containing all the Orders</returns>
        public OrderHistory GetHistory(string username) {
            if (users.Single(s => s.Name == username) == null)
                return null;

            return new OrderHistory {
                UserName = username,
                orders = orders.Where(s => s.User.Name == username).Where(s => s.Completed).OrderBy(s => s.OrderTime)
                .Select(s =>
                    new HistoryOrder {
                        OrderTime = s.OrderTime,
                        CompletionTime = s.CompletionTime,
                        TotalPrice = s.Items.Select(t => t.Item.Count * t.Item.Price).Sum()

                    })
                .ToList()
            };

        }
        /// <summary>
        /// Sets the Accepted flag to the specified value.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="value">New value</param>
        /// <returns>True if the value was correctly changed, false if the Order wasn't found or the old value was equal to the new value</returns>
        public bool SetAccepted(int id, bool value) {
            var item = orders.SingleOrDefault(s => s.Id == id);

            if (item == null)//If item is not found or is already set to the supplied value
                return false;

            var old = item.Accepted;
            item.Accepted = value;
            orders.Update(item);
            return item.Accepted == value && item.Accepted != old;//Ensures the value was correctly set. Returns false if it was already the given value.
        }

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
                Accepted = false,
                Completed = false
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

            orders.Add(order);//Order added


            foreach (var item in order.Items) {
                var temp = items.SingleOrDefault(s => s.Id == item.Item.Id);
                if (temp == null)
                    return false;

                temp.Count -= item.Amount;

                items.Update(temp);//Updated Item
            }

            user.UpdateBalance(-totalPrice);

            users.Update(user);//Updated user

            context.SaveChanges();

            return true;
        }
    }
}
