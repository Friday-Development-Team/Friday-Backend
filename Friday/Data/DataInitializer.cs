using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Friday.Data {
    public class DataInitializer {
        private readonly Context context;
        private readonly UserManager<IdentityUser> userManager;

        public DataInitializer(Context context, UserManager<IdentityUser> userManager) {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task InitializeData(IServiceProvider provider) {


            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated()) {

                await CreateRoles(provider);

                await CreateUser("Admin", null, "T3stP4ssw0rd4dm1n", "Admin");
                await CreateUser("Catering", null, "T3stP4ssw0rdC4t3r1ng", "Catering");
                context.Users.Add(new ShopUser { Balance = 200D, Name = "Admin" });//#TODO Base balance
                context.Users.Add(new ShopUser { Balance = 200D, Name = "Catering" });//#TODO Base balance

                ShopUser user = new ShopUser { Name = "Test", Balance = 200D };
                context.Users.Add(user);
                await CreateUser(user.Name, "Test@Test.test", "Testen", null);
                ShopUser user2 = new ShopUser { Name = "Test2", Balance = 200D };
                context.Users.Add(user2);
                await CreateUser(user2.Name, "Test2@Test.test", "Testen", null);
                context.SaveChanges();


            }

        }

        private async Task CreateUser(string name, string email, string password, string role) {
            var user = new IdentityUser { UserName = name, Email = email };
            await userManager.CreateAsync(user, password);
            if (role != null)
                await userManager.AddToRoleAsync(user, role);
        }

        private async Task CreateRoles(IServiceProvider provider) {
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "Catering" };

            foreach (var role in roles) {
                if (await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
