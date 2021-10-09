using Friday.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Friday.DTOs.Items;

namespace Friday.Data.IServices
{
    /// <summary>
    /// Service that handles Items.
    /// </summary>
    public interface IItemService
    {

        /// <summary>
        /// Retrieves a List of all Item objects.
        /// </summary>
        /// <returns>List of all Items</returns>
        Task<IList<Item>> GetAll();

        /// <summary>
        /// Adds a new Item. ID will be generated.
        /// </summary>
        /// <param name="item">New Item object</param>
        /// <param name="details">ItemDetails object linked to the Item. </param>
        /// <returns>True if the change was successful</returns>
        Task<bool> AddItem(Item item, ItemDetails details);
        /// <summary>
        /// Changes the count of a single Item. Works additively. Use a negative int to subtract. Used for convenience.
        /// </summary>
        /// <param name="user">User that caused the change</param>
        /// <param name="request">Request object for the item change</param>

        /// <returns>True if the change was successful</returns>
        Task<bool> ChangeCount(ShopUser user, ItemAmountChangeRequest request);
        /// <summary>
        /// Changes an Item.
        /// </summary>
        /// <param name="item">Changed Item</param>
        /// <returns>True if the change was successful</returns>
        Task<bool> ChangeItem(Item item);
        /// <summary>
        /// Deletes an Item.
        /// </summary>
        /// <param name="id">ID of the Item</param>
        /// <returns>True if the change was successful</returns>
        Task<bool> DeleteItem(int id);
    }
}
