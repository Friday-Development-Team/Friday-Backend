using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Logs
{
    public class NumericLog : LogBase
    {
        /// <summary>
        /// Amount for this log
        /// </summary>
        public double Count { get; set; }

        /// <summary>
        /// Empty ctor for EF purposes
        /// </summary>
        public NumericLog() : base()
        {

        }

        /// <summary>
        /// Constructs a new NumericLog. 
        /// </summary>
        /// <param name="user">User that placed this log</param>
        /// <param name="count">Amount</param>
        /// <param name="time">Tiùestamp</param>
        public NumericLog(ShopUser user, double count, DateTime time) : base(user, time)
        {
            Count = count;
        }
    }
}
