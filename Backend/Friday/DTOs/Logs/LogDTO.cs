using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Friday.Models.Logs;

namespace Friday.DTOs
{
    /// <summary>
    /// DTO containing data about a log. Used for both currency and items.
    /// </summary>
    public class LogDTO
    {
        /// <summary>
        /// Name of the user (currency) or item.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Amount connected to this log. Amount of currency or item moved
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Timestamp for this log
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// What type of log this is. Used in the frontend for display purposed
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Creates an instance of this DTO from a CurrencyLog.
        /// </summary>
        /// <param name="log">CurrencyLog instance</param>
        /// <returns>DTO with the data from the log</returns>
        public static LogDTO FromCurrencyLog(CurrencyLog log)
        {
            return new LogDTO { Name = log.User.Name, Amount = log.Count, Time = log.Time, Type = LogType.Currency.ToString().ToLower() };
        }

        /// <summary>
        /// Creates an instance of this DTO From an ItemLog.
        /// </summary>
        /// <param name="log">ItemLog instance</param>
        /// <returns>DTO with the data from the log</returns>
        public static LogDTO FromItemLog(ItemLog log)
        {
            return new LogDTO { Name = log.Item.Name, Amount = log.Count, Time = log.Time, Type = LogType.Item.ToString().ToLower() };
        }
    }

    /// <summary>
    /// Type of log presented in a DTO.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Logs involving changes in currency.
        /// </summary>
        Currency,
        /// <summary>
        /// Logs involving items.
        /// </summary>
        Item
    }
}
