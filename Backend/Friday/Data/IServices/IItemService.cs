using System.Collections.Generic;
using Friday.Models;

namespace Friday.Data.IServices {
    public interface IItemService {

        IList<Item> GetAll();
        // ItemDetails GetDetails(int id);
        bool ChangeCount(int id, int amount);

        bool AddItem(Item item, ItemDetails details);
    }
}
