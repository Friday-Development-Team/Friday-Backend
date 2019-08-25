using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Out.Order {
    public class HistoryOrder {
        public double TotalPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime CompletionTime { get; set; }
        public bool Completed { get; set; }
    }
}
