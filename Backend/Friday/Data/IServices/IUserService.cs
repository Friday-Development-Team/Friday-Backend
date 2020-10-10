using Friday.Models;
using Friday.Models.Out;
using System.Collections.Generic;

namespace Friday.Data.IServices
{
    /// <summary>
    /// Service that handles ShopUsers.
    /// </summary>
    public interface IUserService
    {

        /// <summary>
        /// 
        /// Change the balance of the user
        /// 
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <param name="amount">Amount to add to their balance. Negative to subtract</param>
        /// <param name="log">Whether or not a log should be made for this transaction. Default true.</param>
        /// <returns>True if the change was successful</returns>
        bool ChangeBalance(int id, double amount, bool log = true);

        /// <summary>
        /// Changes the balance of the user
        /// </summary>
        /// <param name="name">Username of the user</param>
        /// <param name="amount">Amount to add to their balance. Negative to subtract</param>
        /// <param name="log">Whether or not a log should be made for this transaction. Default true.</param>
        /// <returns>True if the change was successful</returns>
        bool ChangeBalance(string name, double amount, bool log = true);

        ///// <summary>
        ///// 
        ///// Retrieves personal user orders
        ///// 
        ///// </summary>
        ///// <param name="id">ID of the user</param>
        ///// <param name="orderDate"></param>
        ///// <returns></returns>
        //ICollection<Order> GetOrderHistory(int id, DateTime orderDate);
        //Method unused and moved to OrderService instead

        /// <summary>
        /// 
        /// Returns the basic information of the user with the specified user name.
        /// 
        /// </summary>
        /// <returns>Object containing username and balance</returns>
        ShopUserDTO GetUser(string username);

        /// <summary>
        /// 
        /// Adds a User
        /// 
        /// </summary>
        /// <param name="user">User to be added</param>
        bool AddUser(ShopUser user);

        /// <summary>
        /// Returns a list of all users
        /// </summary>
        /// <returns>List of all users</returns>
        IList<ShopUserDTO> GetAll();

        /// <summary>
        /// Retrieves a ShopUser by their username. Should be only be used for internal purposes.
        /// </summary>
        /// <returns>User</returns>
        ShopUser GetByUsername(string username);

    }
}
