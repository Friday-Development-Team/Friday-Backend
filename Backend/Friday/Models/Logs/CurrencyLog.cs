using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models
{
    public class CurrencyLog
    {
        public int Id { get; set; }
        public ShopUser User { get; set; }
        public int UserId { get; set; }
        public double Count { get; set; }
        public DateTime Time { get; set; }
    }
}
