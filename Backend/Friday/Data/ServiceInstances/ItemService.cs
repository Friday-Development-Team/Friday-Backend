using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Logs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.DTOs.Items;

namespace Friday.Data.ServiceInstances
{/// <inheritdoc cref="IItemService" />
    public class ItemService : ServiceBase, IItemService
    {
        private readonly DbSet<Item> items;
        private readonly DbSet<ItemDetails> itemDetails;
        private readonly DbSet<ItemLog> logs;

        /// <summary>
        /// Service for Items.
        /// </summary>
        /// <param name="context">Link to DB</param>
        public ItemService(Context context) : base(context)
        {
            items = this.context.Items;
            itemDetails = this.context.ItemDetails;
            logs = this.context.ItemLogs;

        }
        /// <inheritdoc />
        public async Task<IList<Item>> GetAll()
        {
            return await items.Include(s => s.ItemDetails).AsNoTracking().ToListAsync();

        }

        /// <inheritdoc />
        public async Task<bool> ChangeCount(ShopUser user, ItemAmountChangeRequest request)
        {
            if (user == null)
                throw new Exception();

            var item = await items.SingleAsync(s => s.Id == request.Id);

            if ((request.Amount < 0 && Math.Abs(request.Amount) > item.Count))//Avoid negative numbers
                throw new ArgumentException("You can't change an Item's count below zero!");

            item.Count += request.Amount;

            items.Update(item);

            var returnamount= await context.SaveChangesAsync() > 0;

            await LogItem(user, item, request.Amount);

            return returnamount;
        }
        /// <inheritdoc />
        private async Task<bool> LogItem(ShopUser user, Item item, int amount)
        {
            var log = new ItemLog(user, amount, item);
            await logs.AddAsync(log);
            return await context.SaveChangesAsync() > 0;
        }
        /// <inheritdoc />
        public async Task<bool> AddItem(Item item, ItemDetails details)
        {
            var result = await itemDetails.AddAsync(details);
            await context.SaveChangesAsync();

            item.ItemDetails = result.Entity;
            await items.AddAsync(item);//Add Item itself to data
            await context.SaveChangesAsync();//Save to generate the ID

            return item.Id != 0 && details.Id != 0 && await items.ContainsAsync(item);//Check if Item was successfully added and all values generated. This ensures proper saving.
        }
        /// <inheritdoc />
        public async Task<bool> DeleteItem(int id)
        {
            var item = await items.SingleAsync(s => s.Id == id);
            items.Remove(item);
            return await context.SaveChangesAsync() > 0; //False if nothing was written and the operation failed.

        }
        /// <inheritdoc />
        public async Task<bool> ChangeItem(Item item)
        {
            var old = await items.SingleAsync(s => item.Id == s.Id);
            old = item;
            items.Update(old);
            return await context.SaveChangesAsync() > 0;//True if at least 1 one line in the DB was changed
        }
    }
}
