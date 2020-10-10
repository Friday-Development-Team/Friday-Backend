using Friday.Models.Out.Order;
using System.Collections.Generic;

namespace Friday.Models.Out
{
    /// <summary>
    /// Contains the full history of completed Orders placed by a User.
    /// </summary>
    public class OrderHistory
    {
        /// <summary>
        /// Username of the User
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// List of Orders places by the User.
        /// </summary>
        public IList<HistoryOrder> Orders { get; set; }
    }
}
