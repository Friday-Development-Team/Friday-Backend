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
        public ShopUser User { get; set; }
        public string Status { get; set; }
    }
}
