using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IRepositories;
using Friday.Models;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.RepositoryInstances {
    public class UserRepository : IUserRepository {

        private readonly Context context;
        private readonly DbSet<ShopUser> users;

        public UserRepository(Context context) {
            this.context = context;
            users = this.context.users;
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
