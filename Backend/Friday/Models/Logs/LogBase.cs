using System;

namespace Friday.Models.Logs
{
    /// <summary>
    /// Base for all Log types
    /// </summary>
    public abstract class LogBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User that causes this log to be placed
        /// </summary>
        public ShopUser ShopUser { get; set; }
        /// <summary>
        /// Timestamp for this log
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Empty ctor for EF
        /// </summary>
        protected LogBase() { }

        /// <summary>
        /// Constructs a new LogBase instance. 
        /// </summary>
        /// <param name="user">User that placed this log</param>
        protected LogBase(ShopUser user)
        {
            ShopUser = user;
            Time = DateTime.Now;
        }
    }
}
