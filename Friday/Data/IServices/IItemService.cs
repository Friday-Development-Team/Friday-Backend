using System.Collections.Generic;
using Friday.Models;

namespace Friday.Data.IServices {
    public interface IItemService {

        IList<Item> GetAll();
        ItemDetails GetDetails(int id);
        

    }
}
