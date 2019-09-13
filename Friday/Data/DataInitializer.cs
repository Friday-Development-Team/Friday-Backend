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

        public async Task InitializeData() {
            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated()) {
                ShopUser user = new ShopUser { Name = "Test", Balance = 200D };
                context.Users.Add(user);
                await CreateUser(user.Name, "Test@Test.test", "Testen");
                ShopUser user2 = new ShopUser { Name = "Test2", Balance = 200D };
                context.Users.Add(user2);
                await CreateUser(user2.Name, "Test2@Test.test", "Testen");
                context.SaveChanges();
            }

        }

        private async Task CreateUser(string name, string email, string password) {
            var user = new IdentityUser { UserName = name, Email = email };
            await userManager.CreateAsync(user, password);
        }
    }
}
