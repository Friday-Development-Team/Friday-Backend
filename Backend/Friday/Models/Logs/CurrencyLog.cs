using Friday.Models.Logs;
using System;

namespace Friday.Models
{
    /// <summary>
    /// Contains the information of a single transaction involving currency. Tracks who made the transaction, the amount (negative for subtraction, positive for addition) and when it took place.
    /// </summary>
    public class CurrencyLog : NumericLog
    {
        /// <summary>
        /// Used for EF.
        /// </summary>
        public CurrencyLog() : base() { }

        /// <summary>
        /// Creates a new instance of CurrencyLog, timestamped to the moment of creation of this Log.
        /// </summary>
        /// <param name="user">User whose balance was changed</param>
        /// <param name="amount">Amount to be added. Negative means subtraction</param>
        public CurrencyLog(ShopUser user, double amount) : base(user, amount) { }
    }
}
