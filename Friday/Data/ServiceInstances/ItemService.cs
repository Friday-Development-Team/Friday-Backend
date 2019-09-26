using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Friday.Models.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Friday.Data.ServiceInstances
{
    public class ItemService : IItemService
    {

        //private IList<Item> list;
        //#TODO Inject context
        private readonly Context context;
        private readonly DbSet<Item> items;
        private readonly DbSet<ItemLog> logs;

        public ItemService(Context context)
        {
            this.context = context;
            items = this.context.Items;
            logs = this.context.ItemLogs;

        }
        public IList<Item> GetAll()
        {
            return items.Include(s => s.ItemDetails).AsNoTracking().ToList();
        }
        ///// <summary>
        ///// Returns the details of a specified Item
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>ItemDetails, null if not found</returns>
        //public ItemDetails GetDetails(int id) {
        //    return details.AsNoTracking().SingleOrDefault(s => s.ItemId == id);
        //}
        /// <summary>
        /// Changes the Count of an Item. Will Add the specified amount to the Count. Will subtract if amount if negative. Addition/subtraction is needed to avoid concurrency issues.
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ChangeCount(int id, int amount)
        {
            var item = items.SingleOrDefault(s => s.Id == id);
            if (item == null || (amount < 0 && Math.Abs(amount) > item.Count))//Avoid negative numbers
                return false;
            item.Count += amount;

            items.Update(item);

            context.SaveChanges();

            this.LogItem(item, amount);

            return true;
        }

        private void LogItem(Item item, int count)
        {
            var log = new ItemLog { Item = item, Count = count, Time = DateTime.Now };
            logs.Add(log);
            context.SaveChanges();
        }
    }
}
