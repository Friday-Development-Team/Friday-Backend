using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models {
    public class OrderItem {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public int Amount { get; set; }
    }
}
