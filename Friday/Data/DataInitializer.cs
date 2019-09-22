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
        private readonly RoleManager<IdentityRole> roleManager;

        public DataInitializer(Context context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task InitializeData() {


            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated()) {

                await CreateRoles();

                await CreateUser("Admin", null, "T3stP4ssw0rd4dm1n", "Admin");
                await CreateUser("Catering", null, "T3stP4ssw0rdC4t3r1ng", "Catering");
                await CreateUser("Kitchen", null, "T3stP4ssw0rdK1tch3n", "Kitchen");
                context.ShopUsers.Add(new ShopUser { Balance = 200D, Name = "Admin" });//#TODO Base balance
                context.ShopUsers.Add(new ShopUser { Balance = 200D, Name = "Catering" });//#TODO Base balance
                context.ShopUsers.Add(new ShopUser { Balance = 200D, Name = "Kitchen" });

                ShopUser user = new ShopUser { Name = "Test", Balance = 200D };
                context.ShopUsers.Add(user);
                await CreateUser(user.Name, "Test@Test.test", "Testen", "User");
                ShopUser user2 = new ShopUser { Name = "Test2", Balance = 200D };
                context.ShopUsers.Add(user2);
                await CreateUser(user2.Name, "Test2@Test.test", "Testen", "User");
                context.SaveChanges();


            }

        }

        private async Task CreateUser(string name, string email, string password, string role) {
            var user = new IdentityUser { UserName = name, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded) {
                var createdUser = await userManager.FindByNameAsync(user.UserName);
                await userManager.AddToRoleAsync(createdUser, role);
            }

        }

        private async Task CreateRoles() {
            string[] roles = { "Admin", "Catering", "Kitchen", "User" };

            foreach (var role in roles) {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
