using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Friday.Models.Logs;

namespace Friday.DTOs {
    public class LogDTO {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime Time { get; set; }
        public LogType Type { get; set; }

        public static LogDTO FromCurrencyLog(CurrencyLog log) {
            return new LogDTO { Name = log.User.Name, Amount = log.Count, Time = log.Time, Type = LogType.Currency};
        }

        public static LogDTO FromItemLog(ItemLog log) {
            return new LogDTO { Name = log.Item.Name, Amount = log.Amount, Time = log.Time, Type = LogType.Item};
        }
    }

    public enum LogType {
        Currency,
        Item
    }
}
