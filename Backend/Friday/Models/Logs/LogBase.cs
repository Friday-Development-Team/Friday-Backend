using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Friday.Models.Logs
{
    /// <summary>
    /// Base for all Log types
    /// </summary>
    public class LogBase
    {
        public int Id { get; set; }
        /// <summary>
        /// User that causes this log to be placed
        /// </summary>
        public ShopUser User { get; set; }
        /// <summary>
        /// ID of the User
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Timestamp for this log
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Empty ctor for EF
        /// </summary>
        public LogBase()
        {

        }

        /// <summary>
        /// Constructs a new LogBase instance. 
        /// </summary>
        /// <param name="user">User that placed this log</param>
        /// <param name="time">Timestamp</param>
        public LogBase(ShopUser user, DateTime time)
        {
            User = user;
            UserId = User.Id;
            Time = time;
        }
    }
}
