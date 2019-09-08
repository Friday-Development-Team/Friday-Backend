using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances {
    public class ItemService : IItemService {

        //private IList<Item> list;
        //#TODO Inject context
        private readonly Context context;
        private readonly DbSet<Item> items;
        private readonly DbSet<ItemDetails> details;

        public ItemService(Context context) {
            this.context = context;
            this.items = this.context.items;
            details = this.context.itemDetails;
            //Inject
            //list = new List<Item>();
            //list.Add(new Item { Id = 1, Count = 50, Name = "Water", Price = 1.5, Type = "Beverage" });
            //list.Add(new Item { Id = 2, Count = 50, Name = "Cola", Price = 1.5, Type = "Beverage" });
            //list.Add(new Item { Id = 3, Count = 50, Name = "Cola Light", Price = 1.5, Type = "Beverage" });
            //list.Add(new Item { Id = 4, Count = 50, Name = "Cola Zero", Price = 1.5, Type = "Beverage" });
            //list.Add(new Item { Id = 5, Count = 50, Name = "Ice Tea", Price = 1.5, Type = "Beverage" });
            //list.Add(new Item { Id = 6, Count = 50, Name = "Hot Dog", Price = 1.5, Type = "Food" });
            //list.Add(new Item { Id = 7, Count = 50, Name = "Pizza", Price = 1.5, Type = "Food" });
            //list.Add(new Item { Id = 8, Count = 50, Name = "Pizza Bolognese", Price = 1.5, Type = "Food" });
        }
        public IList<Item> GetAll() {
            return items.AsNoTracking().ToList();
        }
        /// <summary>
        /// Returns the details of a specified Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ItemDetails, null if not found</returns>
        public ItemDetails GetDetails(int id) {
            return details.AsNoTracking().SingleOrDefault(s => s.ItemId == id);
        }
        /// <summary>
        /// Changes the Count of an Item. Will Add the specified amount to the Count. Will subtract if amount if negative. Addition/subtraction is needed to avoid concurrency issues.
        /// </summary>
        /// <param name="id">Id of the item</param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ChangeCount(int id, int amount) {
            var item = items.SingleOrDefault(s => s.Id == id);
            if (item == null || (amount < 0 && Math.Abs(amount) > item.Count))//Avoid negative numbers
                return false;
            item.Count += amount;

            items.Update(item);

            context.SaveChanges();

            return true;
        }
    }
}
