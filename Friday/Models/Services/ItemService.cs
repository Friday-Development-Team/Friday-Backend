using Friday.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models.Services {
    public class ItemService {
        private IItemRepository repo;

        public ItemService(IItemRepository repo) {
            this.repo = repo;
        }

        IList<Item> GetAll() {
            return repo.GetAll();
        }

        ItemDetails GetDetails(int id) {
            return repo.GetDetails(id);
        }
    }
}
