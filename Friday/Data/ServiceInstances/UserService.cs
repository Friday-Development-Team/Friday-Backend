using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances {
    public class UserService : IUserService {

        private readonly Context context;
        private readonly DbSet<ShopUser> users;

        public UserService(Context context) {
            this.context = context;
            users = this.context.Users;
        }

        public bool ChangeBalance(int id, double amount) {
            var user = users.SingleOrDefault(s => s.Id == id);
            if (user.UpdateBalance(amount))
                return false;
            users.Update(user);
            return true;
        }
    }
}
