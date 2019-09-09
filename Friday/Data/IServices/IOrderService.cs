using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;

namespace Friday.Data.IServices {
    public interface IOrderService {
        /// <summary>
        /// Returns all the completed Orders of a specified user.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>Object containing all the Orders</returns>
        OrderHistory GetHistory(string username);

        /// <summary>
        /// Sets the Accepted flag to the specified value.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="value">New value</param>
        /// <returns>True if the value was correctly changed, false if the Order wasn't found or the old value was equal to the new value</returns>
        bool SetAccepted(int id, bool value);
        /// <summary>
        /// Places an Order. Checks if the Order is valid and can be placed.
        /// </summary>
        /// <param name="orderdto">DTO</param>
        /// <returns>True if the Order is valid and could be placed</returns>
        bool PlaceOrder(OrderDTO orderdto);
        /// <summary>
        /// Sets the Order status to completed.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if successful</returns>
        bool SetCompleted(int id);
        /// <summary>
        /// Cancels an Order. An Order can only be cancelled if it isn't Completed or, depending on configuration, not Accepted.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <returns>True if successful</returns>
        bool Cancel(int id);

    }
}
