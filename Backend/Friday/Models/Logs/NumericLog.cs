using System;

namespace Friday.Models.Logs
{
    /// <summary>
    /// Base type for logs tracking a numeric value, such as money or stock.
    /// </summary>
    public abstract class NumericLog : LogBase
    {
        /// <summary>
        /// Amount for this log
        /// </summary>
        public double Count { get; set; }

        /// <summary>
        /// Empty ctor for EF purposes
        /// </summary>
        protected NumericLog() : base() { }

        /// <summary>
        /// Constructs a new NumericLog. 
        /// </summary>
        /// <param name="user">User that placed this log</param>
        /// <param name="count">Amount</param>
        protected NumericLog(ShopUser user, double count) : base(user)
        {
            Count = count;
        }
    }
}
