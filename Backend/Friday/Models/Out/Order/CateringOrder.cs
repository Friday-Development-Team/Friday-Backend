using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Friday.Models.Out.Order
{
    /// <summary>
    /// Contains the data of an ongoing, not-completed Order. Used to give whoever handles the catering all the needed information.
    /// </summary>
    public class CateringOrder
    {
        /// <summary>
        /// ID of the Order
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// List of Items ordered, stored in an object that only exposes necessary information
        /// </summary>
        public IList<HistoryOrderItem> Items { get; set; }
        /// <summary>
        /// Time the Order was placed
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// User that placed this Order
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Where said User is seated
        /// </summary>
        public string UserSeat { get; set; }
        /// <summary>
        /// Status of the beverages
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus StatusBeverage { get; set; }
        /// <summary>
        /// Status of the food
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus StatusFood { get; set; }
        /// <summary>
        /// Total amount paid for the Order, pre-calculated for convenience.
        /// </summary>
        public double TotalPrice { get; set; }

    }
}
