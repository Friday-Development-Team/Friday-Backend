using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models.Logs;

namespace Friday.Models
{
    /// <summary>
    /// Class containing information about Items used in the shop. Contains only basic information, such as name, price, type (food or drink) and the amount left.
    /// </summary>
    public class Item
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public ItemDetails ItemDetails { get; set; }
        public ICollection<ItemLog> Logs { get; set; }
    }
}
