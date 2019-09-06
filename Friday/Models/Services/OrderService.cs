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
        /// Returns a list of all the orders placed by a user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public OrderHistory GetHistory(string username) {
            //Check if user exists
            //if(userRepo.GetByName(username)==null)
            //return null;
            return orderRepo.GetHistory(username);
        }

        public async Task<bool> SetAccept(int id, bool value) {
            return await orderRepo.SetAccept(id, value);
        }


    }
}
