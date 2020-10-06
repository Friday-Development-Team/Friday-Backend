using Friday.Models.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public CurrencyLog() : base()
        {

        }

        /// <summary>
        /// Creates a new instance of CurrencyLog.
        /// </summary>
        /// <param name="user">User whose balance was changed</param>
        /// <param name="amount">Amount to be added. Negative means subtraction</param>
        /// <param name="time">Timestamp</param>
        public CurrencyLog(ShopUser user, double amount, DateTime time) : base(user, amount, time)
        {
        }
    }
}
