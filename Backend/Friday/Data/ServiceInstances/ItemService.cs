using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Friday.Data.ServiceInstances
{/// <inheritdoc cref="IItemService" />
    public class ItemService : ServiceBase, IItemService
    {
        private readonly DbSet<Item> items;
        private readonly DbSet<ItemLog> logs;

        /// <summary>
        /// Service for Items.
        /// </summary>
        /// <param name="context">Link to DB</param>
        public ItemService(Context context) : base(context)
        {
            items = this.context.Items;
            logs = this.context.ItemLogs;

        }
        /// <inheritdoc />
        public IList<Item> GetAll()
        {
            var result = items.Include(s => s.ItemDetails).AsNoTracking().ToList();
            return result;
        }
        ///// <summary>
        ///// Returns the details of a specified Item
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>ItemDetails, null if not found</returns>
        //public ItemDetails GetDetails(int id) {
        //    return details.AsNoTracking().SingleOrDefault(s => s.ItemId == id);
        //}


        /// <inheritdoc />
        public bool ChangeCount(ShopUser user, int id, int amount)
        {
            if (user == null)
                return false;
            var item = items.SingleOrDefault(s => s.Id == id);
            if (item == null || (amount < 0 && Math.Abs(amount) > item.Count))//Avoid negative numbers
                return false;
            item.Count += amount;

            items.Update(item);

            context.SaveChanges();

            LogItem(user, item, amount);

            return true;
        }
        /// <inheritdoc />
        private void LogItem(ShopUser user, Item item, int amount)
        {
            var log = new ItemLog(user, amount, DateTime.Now, item);
            logs.Add(log);
            context.SaveChanges();
        }
        /// <inheritdoc />
        public bool AddItem(Item item, ItemDetails details)
        {
            items.Add(item);//Add Item itself to data
            context.SaveChanges();//Save to generate the ID

            details.ItemId = item.Id;//Set ItemId with newly generated value from Item

            items.Update(item);//Sets the Item as updated to new Details are saved too
            context.SaveChanges();//Save to generated ID

            return item.Id != 0 && details.Id != 0 && items.Contains(item);//Check if Item was successfully added and all values generated. This ensures proper saving.
        }
        /// <inheritdoc />
        public bool DeleteItem(int id)
        {
            var item = items.SingleOrDefault(s => s.Id == id);
            if (item == null)
                return false;
            items.Remove(item);
            return context.SaveChanges() > 0;//False if nothing was written and the operation failed.

        }
        /// <inheritdoc />
        public bool ChangeItem(Item item)
        {
            var old = items.Single(s => item.Id == s.Id);
            old = item;
            items.Update(old);
            return context.SaveChanges() > 0;//True if at least 1 one line in the DB was changed
        }
    }
}
