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

        public bool IsOngoing() {
            return Status == OrderStatus.Pending || Status == OrderStatus.Accepted ||
                   Status == OrderStatus.SentToKitchen;
        }
    }
    /// <summary>
    /// Represents the Status of an Order
    /// </summary>
    public enum OrderStatus {
        /// <summary>
        /// Order has been Accepted and is being handled
        /// </summary>
        Accepted,//Should be first to be able to order all orders
        /// <summary>
        /// Order is placed but hasn't been accepted yet
        /// </summary>
        Pending,
        /// <summary>
        /// 
        /// </summary>
        SentToKitchen,
        /// <summary>
        /// Order has been completed 
        /// </summary>
        Completed,
        /// <summary>
        /// Order has been cancelled and cannot be processed in any way
        /// </summary>
        Cancelled
    }
}
