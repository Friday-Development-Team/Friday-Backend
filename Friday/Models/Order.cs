using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models {
    public class Order {
        public int UserId { get; set; }
        public ShopUser User { get; set; }
        public IList<OrderItem> Items { get; set; }
        public DateTime Time { get; set; }
        public bool Accepted { get; set; }
        public bool Completed { get; set; }
    }
}
