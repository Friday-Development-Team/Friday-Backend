using Friday.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Data
{
    /// <summary>
    /// Used to initialize all the needed data on startup.
    /// </summary>
    public class DataInitializer
    {
        private readonly Context context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Initializes Database data.
        /// </summary>
        /// <param name="context">Context managing the data.</param>
        /// <param name="userManager">Managers IdentityUsers</param>
        /// <param name="roleManager">Manages roles</param>
        public DataInitializer(Context context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Initializes and seeds all the data. Checks if the database already exists and creates it if needed.
        /// </summary>
        /// <returns>Task</returns>
        public async Task InitializeData()
        {


            //context.Database.EnsureDeleted();//Comment out to avoid renewal of all data.
            if (context.Database.EnsureCreated())
            {

                await CreateRoles();

                await CreateUser("Admin", null, "T3stP4ssw0rd4dm1n", Role.Admin);
                await CreateUser("Catering", null, "T3stP4ssw0rdC4t3r1ng", Role.Catering);
                await CreateUser("Kitchen", null, "T3stP4ssw0rdK1tch3n", Role.Kitchen);

                context.ShopUsers.Add(new ShopUser { Balance = 200D, Name = "Admin", Seat = "Entrance" });
                context.ShopUsers.Add(new ShopUser { Balance = 200D, Name = "Catering", Seat = "Bar" });
                context.ShopUsers.Add(new ShopUser { Balance = 200D, Name = "Kitchen", Seat = "Kitchen" });

                ShopUser user = new ShopUser { Name = "Test", Balance = 200D, Seat = "A1" };
                context.ShopUsers.Add(user);
                await CreateUser(user.Name, "Test@Test.test", "Testen", Role.User);
                ShopUser user2 = new ShopUser { Name = "Test2", Balance = 200D, Seat = "A2" };
                context.ShopUsers.Add(user2);
                await CreateUser(user2.Name, "Test2@Test.test", "Testen", Role.User);

                SeedItems();

                context.Configuration.Add(new Configuration());

                context.SaveChanges();

            }

        }

        /// <summary>
        /// Seeds all the Item objects.
        /// </summary>
        private void SeedItems()
        {
            Item item = new Item
            {
                Name = "Water",
                Price = 1.0,
                Count = 50,
                Type = ItemType.Beverage,
                NormalizedImageName = "water",
                ItemDetails = new ItemDetails
                {
                    Allergens = "",
                    Calories = 0D,
                    SaltContent = 0D,
                    Size = "200ml",
                    SugarContent = 0D
                }
            };
            context.Items.Add(item);

            item = new Item
            {
                Name = "Cola",
                Price = 2.0,
                Count = 50,
                Type = ItemType.Beverage,
                NormalizedImageName = "cola",
                ItemDetails = new ItemDetails
                {
                    Allergens = "",
                    Calories = 50D,
                    SaltContent = 0D,
                    Size = "200ml",
                    SugarContent = 50D
                }
            };
            context.Items.Add(item);

            item = new Item
            {
                Name = "Cola Light",
                Price = 2.0,
                Count = 50,
                Type = ItemType.Beverage,
                NormalizedImageName = "cola_light",
                ItemDetails = new ItemDetails
                {
                    Allergens = "",
                    Calories = 0D,
                    SaltContent = 0D,
                    Size = "200ml",
                    SugarContent = 0D
                }
            };

            context.Items.Add(item);

            item = new Item
            {
                Name = "Cola Zero",
                Price = 2.0,
                Count = 50,
                Type = ItemType.Beverage,
                NormalizedImageName = "cola_zero",
                ItemDetails = new ItemDetails
                {
                    Allergens = "",
                    Calories = 0D,
                    SaltContent = 0D,
                    Size = "200ml",
                    SugarContent = 0D
                }
            };

            context.Items.Add(item);

            item = new Item
            {
                Name = "Red Bull",
                Price = 2.5,
                Count = 50,
                Type = ItemType.Beverage,
                NormalizedImageName = "red_bull",
                ItemDetails = new ItemDetails
                {
                    Allergens = "Taurine, Caffeïne",
                    Calories = 30D,
                    SaltContent = 0D,
                    Size = "250ml",
                    SugarContent = 20D
                }
            };

            context.Items.Add(item);

            item = new Item
            {
                Name = "Fanta",
                Price = 2.0,
                Count = 50,
                Type = ItemType.Beverage,
                NormalizedImageName = "fanta",
                ItemDetails = new ItemDetails
                {
                    Allergens = "",
                    Calories = 50D,
                    SaltContent = 0D,
                    Size = "200ml",
                    SugarContent = 20D
                }
            };

            context.Items.Add(item);

            item = new Item
            {
                Name = "Jupiler",
                Price = 2.0,
                Count = 50,
                Type = ItemType.Beverage,
                NormalizedImageName = "jupiler",
                ItemDetails = new ItemDetails
                {
                    Allergens = "",
                    Calories = 50D,
                    SaltContent = 0D,
                    Size = "330ml",
                    SugarContent = 0D
                }
            };

            context.Items.Add(item);

            item = new Item
            {
                Name = "Pizza",
                Price = 4.0,
                Count = 50,
                Type = ItemType.Food,
                NormalizedImageName = "pizza",
                ItemDetails = new ItemDetails
                {
                    Allergens = "Cheese, Meat, Gluten",
                    Calories = 500D,
                    SaltContent = 20D,
                    Size = "1 full pizza",
                    SugarContent = 0D
                }
            };

            context.Items.Add(item);



        }

        /// <summary>
        /// Creates a User account for authentication purposes
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private async Task CreateUser(string name, string email, string password, string role)
        {
            var user = new IdentityUser { UserName = name, Email = email };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var createdUser = await userManager.FindByNameAsync(user.UserName);
                if (name != "Admin")
                    await userManager.AddToRoleAsync(createdUser, role);
                else
                    await userManager.AddToRolesAsync(createdUser, roleManager.Roles.Select(s => s.Name));
            }

        }

        /// <summary>
        /// Creates all the roles needed.
        /// </summary>
        /// <returns></returns>
        private async Task CreateRoles()
        {
            string[] roles = { Role.Admin, Role.Catering, Role.Kitchen, Role.User };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
