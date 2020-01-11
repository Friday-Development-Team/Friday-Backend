using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Out.Order
{
    public class CateringOrderDTO
    {
        public int Id { get; set; }
        public IList<HistoryOrderItem> Items { get; set; }
        public DateTime OrderTime { get; set; }
        public string User { get; set; }
        public string StatusBeverage { get; set; }
        public string StatusFood { get; set; }
        public double TotalPrice { get; set; }

    }
}
