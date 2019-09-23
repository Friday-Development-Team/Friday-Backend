using Friday.Models;
using System;
using System.Collections.Generic;

namespace Friday.Data.IServices {
    public interface IUserService {

        /// <summary>
        /// 
        /// Change the balance of the user
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool ChangeBalance(int id, double amount);

        /// <summary>
        /// 
        /// Retrieve personal user orders
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderDate"></param>
        /// <returns></returns>
        ICollection<Order> GetOrderHistory(int id, DateTime orderDate);

        /// <summary>
        /// 
        /// Needed to display the logged in user (show name and balance)
        /// 
        /// </summary>
        /// <returns></returns>
        ShopUser GetUser(string username);

        /// <summary>
        /// 
        /// Adds a User
        /// 
        /// </summary>
        /// <param name="user">User to be added</param>
        bool AddUser(ShopUser user);

    }
}
