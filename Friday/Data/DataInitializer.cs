using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Microsoft.AspNetCore.Identity;

namespace Friday.Data {
    public class DataInitializer {
        private readonly Context context;
        private readonly UserManager<IdentityUser> userManager;

        public DataInitializer(Context context, UserManager<IdentityUser> userManager) {
            this.context = context;
            this.userManager = userManager;
        }

        public async void InitializeData() {
            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated()) {
                //seeding the database with recipes, see DBContext         
                ShopUser user = new ShopUser { Name = "Test", Balance = 200D };
                context.users.Add(user);
                await CreateUser(user.Name, "Testen");
                ShopUser user2 = new ShopUser { Name = "Test2", Balance = 200D };
                context.users.Add(user2);
                await CreateUser(user2.Name, "Testen");
                context.SaveChanges();
            }

        }

        private async Task CreateUser(string name, string password) {
            var user = new IdentityUser { UserName = name };
            await userManager.CreateAsync(user, password);
        }
    }
}
