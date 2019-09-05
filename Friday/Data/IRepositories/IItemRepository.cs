using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;

namespace Friday.Data.IRepositories {
    public interface IItemRepository {

        IList<Item> GetAll();
        ItemDetails GetDetails(int id);
        

    }
}
