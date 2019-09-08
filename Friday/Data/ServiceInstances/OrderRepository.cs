using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Models;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances {
    public class OrderService : IOrderService {

        private readonly Context context;
        private readonly DbSet<Order> orders;

        public OrderService(Context context) {
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
