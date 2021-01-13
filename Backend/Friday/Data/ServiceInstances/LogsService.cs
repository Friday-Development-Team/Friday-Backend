using Friday.Data.IServices;
using Friday.DTOs;
using Friday.Models;
using Friday.Models.Logs;
using Friday.Models.Out;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Data.ServiceInstances
{
    /// <inheritdoc cref="ILogsService" />
    public class LogsService : ServiceBase, ILogsService
    {

        private readonly DbSet<CurrencyLog> currencyLogs;
        private readonly DbSet<ItemLog> itemLogs;
        private readonly DbSet<Item> items;
        /// <summary>
        /// Service for all types of Logs.
        /// </summary>
        /// <param name="ctext">Link to DB</param>
        public LogsService(Context ctext) : base(ctext)
        {
            currencyLogs = context.CurrencyLogs;
            itemLogs = context.ItemLogs;
            items = context.Items;
        }

        /// <inheritdoc />
        public async Task<IList<LogDTO>> GetAllCurrencyLogs()
        {
            return await currencyLogs.Include(s => s.ShopUser).AsNoTracking().Select(s => LogDTO.FromCurrencyLog(s)).ToListAsync();
        }
        /// <inheritdoc />
        public async Task<IList<LogDTO>> GetAllItemLogs()
        {
            return await itemLogs.Include(s => s.Item).AsNoTracking().Select(s => LogDTO.FromItemLog(s)).ToListAsync();
        }
        /// <inheritdoc />
        public async Task<IList<LogDTO>> GetByUser(int id)
        {
            if (await currencyLogs.AllAsync(s => s.ShopUser.Id != id))
                throw new ArgumentException();

            return await currencyLogs.Include(s => s.ShopUser)
                .Where(s => s.ShopUser.Id == id)
                .OrderBy(s => s.Time)
                .Select(s => LogDTO.FromCurrencyLog(s)).ToListAsync();
        }
        /// <inheritdoc />
        public async Task<IList<LogDTO>> GetPerItem(int id)
        {
            if (await items.AllAsync(s => s.Id != id))
                throw new ArgumentException();
            return await itemLogs.AsNoTracking().Include(s => s.Item)
                .Where(s => s.Item.Id == id)
                .Select(s => LogDTO.FromItemLog(s))
                .ToListAsync();
        }
        /// <inheritdoc />
        public async Task<IList<ItemAmountDTO>> GetRemainingStock()
        {
            return await items.Select(s => new ItemAmountDTO { Item = s, Amount = s.Count }).ToListAsync();
        }
        /// <inheritdoc />
        public Task<double> GetTotalIncome()
        {
            return currencyLogs.Where(s => s.Count > 0).SumAsync(s => s.Count);//Only positive amounts. Those logs mean that money was added, thus income.
        }
        /// <inheritdoc />
        public async Task<IList<ItemAmountDTO>> GetTotalStockSold()
        {
            //var result = itemLogs.Where(s => s.Amount < 0).ToList();
            //return result.ToDictionary(s => s.Item, s => result.Where(t => t.Equals(s))).Select(s => new ItemAmountDTO { Item = s.Key, Amount = s.Value.Sum(t => t.Amount) }).ToList();
            return await itemLogs.AsNoTracking()
                .Where(s => s.Count < 0)// Subtractions only
                .GroupBy(s => s.Item)
                .Select(s =>
                    new ItemAmountDTO
                    {
                        Item = s.Key, 
                        Amount = Math.Abs(Convert.ToInt32(s.Sum(t => t.Count)))
                    })
                .ToListAsync();
        }

    }
}
