using System;

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

        /// <summary>
        /// Empty ctor used for EF. Don't use directly.
        /// </summary>
        public ItemLog() : base()
        {

        }

        /// <summary>
        /// Creates a new instance of this log
        /// </summary>
        /// <param name="user">User that placed this log</param>
        /// <param name="amount">Amount of items in the transaction. Positive for an addition of items, negative for items being sold</param>
        /// <param name="time">Timestamp</param>
        /// <param name="item">Item a log is made about</param>
        public ItemLog(ShopUser user, double amount, DateTime time, Item item) : base(user, amount, time)
        {
            Item = item;
            ItemId = Item.Id;
        }

    }
}