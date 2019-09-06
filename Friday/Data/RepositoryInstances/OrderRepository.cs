using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Models;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.RepositoryInstances {
    public class OrderRepository : IOrderRepository {

        private readonly Context context;
        private readonly DbSet<Order> orders;

        public OrderRepository(Context context) {
            this.context = context;
            this.orders = context.orders;
        }

        public OrderHistory GetHistory(string username) {

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
        /// Updates  the Accepted flag for the given Order. Changes should be saved in order to be applied.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns>True if the order could be found and the old value was not equal to the given value</returns>
        public bool SetAccept(int id, bool value) {
            var item = orders.SingleOrDefault(s => s.Id == id);
            if (item == null || item.Accepted == value)//If item is not found or is already set to the supplied value
                return false;
            item.Accepted = value;
            orders.Update(item);
            return item.Accepted == value;//Ensures the value was correctly set.
        }
    }
}
