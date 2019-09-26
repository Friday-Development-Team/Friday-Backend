using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Logs
{
    public class ItemLog
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int ItemId { get; set; }
        public int Count { get; set; }
        public DateTime Time { get; set; }
    }
}
