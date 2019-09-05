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
                        TotalPrice = s.Items.Select(t => t.Item.Count * t.Item.Price).Sum(),
                        Completed = true //Should be removed, all shown orders are completed, //#remove

                    })
                .ToList()
            };

        }

        public Task<bool> SetAccept(int id, bool value) {
            throw new NotImplementedException();
        }
    }
}
