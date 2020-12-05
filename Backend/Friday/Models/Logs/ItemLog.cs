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
        /// Empty ctor used for EF. This shouldn't be used directly.
        /// </summary>
        public ItemLog() : base() { }

        /// <summary>
        /// Creates a new instance of ItemLog, timestamped to the moment of creation of this Log.
        /// </summary>
        /// <param name="user">User that placed this log</param>
        /// <param name="amount">Amount of items in the transaction. Positive for an addition of items, negative for items being sold</param>
        /// <param name="item">Item a log is made about</param>
        public ItemLog(ShopUser user, double amount, Item item) : base(user, amount)
        {
            Item = item;
            ItemId = Item.Id;
        }

    }
}