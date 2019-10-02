using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models.Out.Order;

namespace Friday.Models.Out {
    public class OrderHistory {
        public string UserName { get; set; }
        public IList<HistoryOrder> orders;
    }
}
