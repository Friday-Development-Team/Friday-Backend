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
        public DateTime CompletionTimeBeverage { get; set; }
        public DateTime CompletionTimeFood { get; set; }
        public OrderStatus StatusBeverage { get; set; }
        public OrderStatus StatusFood { get; set; }

        public bool IsOngoing() {
            return StatusBeverage == OrderStatus.Pending || StatusBeverage == OrderStatus.Accepted ||
                   StatusFood == OrderStatus.SentToKitchen || StatusFood == OrderStatus.Pending ||
                   StatusFood == OrderStatus.Accepted;
        }

        public bool CanBeCancelled(bool OnAccept) {
            return (StatusFood == OrderStatus.Pending && StatusBeverage == OrderStatus.Pending) || (OnAccept && StatusFood == OrderStatus.Accepted && StatusBeverage == OrderStatus.Accepted);
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
        Cancelled,
        /// <summary>
        /// Signals that this Order has no Status. Only applicable when an Order only has beverages and no food or vice versa
        /// </summary>
        None
    }
}
