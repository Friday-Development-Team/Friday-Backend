using Friday.DTOs;
using Friday.Models.Out;
using Friday.Models.Out.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Friday.Data.IServices
{
    /// <summary>
    /// Service that handles Orders or information derived from these Orders.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Returns all the completed Orders of a specified user.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>Object containing all the Orders</returns>
        Task<OrderHistory> GetHistory(string username);

        /// <summary>
        /// Sets the Accepted flag to the specified value.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="value">New value</param>
        /// <param name="toKitchen">Whether or not items should be sent to the kitchen</param>
        /// <returns>True if the value was correctly changed, false if the Order wasn't found or the old value was equal to the new value</returns>
        Task<bool> SetAccepted(int id, bool value, bool toKitchen);

        /// <summary>
        /// Places an Order. Checks if the Order is valid and can be placed.
        /// </summary>
        /// <param name="user">User that placed the Order</param>
        /// <param name="orderdto">DTO</param>
        /// <returns>True if the Order is valid and could be placed</returns>
        Task<int> PlaceOrder(string user, OrderDTO orderdto);

        /// <summary>
        /// Sets the Order status to completed.
        /// </summary>
        /// <param name="id">ID of the Order</param>
        /// <param name="forBeverage">Only complete for beverages or not</param>
        /// <returns>True if successful</returns>
        Task<bool> SetCompleted(int id, bool forBeverage);
        /// <summary>
        /// Cancels an Order. An Order can only be cancelled if it isn't Completed or, depending on configuration, not Accepted.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>True if successful</returns>
        Task<bool> Cancel(int id);
        /// <summary>
        /// Returns the status of the specified Order
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>String representation of the Status of the Order</returns>
        Task<string> GetStatus(int id);
        /// <summary>
        /// Returns a List containing all the non-Completed and non-Cancelled Ordered. Accepted first, then ordered by date asc.
        /// </summary>
        /// <returns>List of all current orders</returns>
        Task<IList<CateringOrder>> GetAll(bool isKitchen);

    }
}
