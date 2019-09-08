using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models {
    public class Order {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ShopUser User { get; set; }
        public IList<OrderItem> Items { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime CompletionTime { get; set; }
        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus {
        Pending,
        Accepted,
        Completed,
        Cancelled
    }
}
