using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Models;

namespace Friday.Data.RepositoryInstances {
    public class ItemRepository : IItemRepository {

        private IList<Item> list
        //#TODO Inject context

        public ItemRepository(/*Inject*/) {
            //Inject
            list = new List<Item>();
            list.Add(new Item { Id = 1, Count = 50, Name = "Water", Price = 1.5, Type = "Beverage" });
            list.Add(new Item { Id = 2, Count = 50, Name = "Cola", Price = 1.5, Type = "Beverage" });
            list.Add(new Item { Id = 3, Count = 50, Name = "Cola Light", Price = 1.5, Type = "Beverage" });
            list.Add(new Item { Id = 4, Count = 50, Name = "Cola Zero", Price = 1.5, Type = "Beverage" });
            list.Add(new Item { Id = 5, Count = 50, Name = "Ice Tea", Price = 1.5, Type = "Beverage" });
            list.Add(new Item { Id = 6, Count = 50, Name = "Hot Dog", Price = 1.5, Type = "Food" });
            list.Add(new Item { Id = 7, Count = 50, Name = "Pizza", Price = 1.5, Type = "Food" });
            list.Add(new Item { Id = 8, Count = 50, Name = "Pizza Bolognese", Price = 1.5, Type = "Food" });
        }
        public IList<Item> GetAll() {
            return list;//#TODO
        }

        public ItemDetails GetDetails(int id) {
            Item item = this.list.Single(s => s.Id == id);//#TODO
            return new ItemDetails { Item = item, ItemId = item.Id, Allergens = "None", Calories = 200F, SaltContent = 0F, Size = "300ml", SugarContent = 0F };
        }
    }
}
