using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models.Logs;

namespace Friday.Models {
    /// <summary>
    /// Class containing information about Items used in the shop. Contains only basic information, such as name, price, type (food or drink) and the amount left.
    /// </summary>
    public class Item {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ItemType Type { get; set; }
        public int Count { get; set; }
        public ItemDetails ItemDetails { get; set; }
        public ICollection<ItemLog> Logs { get; set; }
        public string NormalizedImageName { get; set; }

    }

    public enum ItemType {
        Food,
        Beverage

    }

    public class ItemTools {
        public static ItemType FromString(string s) {
            switch (s.ToUpper()) {
                case "FOOD": return ItemType.Food;
                case "BEVERAGE":
                default: return ItemType.Beverage;
            }
        }
    }
}

