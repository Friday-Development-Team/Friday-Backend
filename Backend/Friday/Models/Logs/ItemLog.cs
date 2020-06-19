using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Logs
{
    /// <summary>
    /// Contains the information of a single transaction involving Items. Tracks what Item was changed, the amount (negative for subtraction, positive for addition) and when it took place.
    /// </summary>
    public class ItemLog : NumericLog
    {
        /// <summary>
        /// Item this log is about
        /// </summary>
        public Item Item { get; set; }
        /// <summary>
        /// ID of the Item
        /// </summary>
        public int ItemId { get; set; }

        public ItemLog() : base()
        {

        }

        public ItemLog(ShopUser user, double amount, DateTime time, Item item) : base(user, amount, time)
        {
            Item = item;
            ItemId = Item.Id;
        }

    }
}
//TODO Add User that used transaction (customer for orders, admin for orders and inventory management)
