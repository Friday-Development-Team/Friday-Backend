using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Data.Unit;
using Friday.Models.Out;

namespace Friday.Models.Services {
    public class OrderService {
        private IOrderRepository orderRepo;
        private IItemRepository itemRepo;
        private IUnitOfWork uow;

        public OrderService(IUnitOfWork uow) {
            this.uow = uow;
            this.orderRepo = uow.orders;
            this.itemRepo = uow.items;
        }
        /// <summary>
        /// Returns a list of all the orders previously placed by the user
        /// </summary>
        /// <param name="username">User</param>
        /// <returns></returns>
        public OrderHistory GetHistory(string username) {
            //Check if user exists
            //if(userRepo.GetByName(username)==null)
            //return null;
            return orderRepo.GetHistory(username);
        }

        /// <summary>
        /// Updates  the Accepted flag for the given Order. Changes should be saved in order to be applied.
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="value">New value</param>
        /// <returns>True if the order could be found and the old value was not equal to the given value</returns>
        public bool SetAccept(int id, bool value) {
            return orderRepo.SetAccept(id, value);
        }


    }
}
