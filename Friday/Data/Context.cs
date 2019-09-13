using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data {
    public class Context : IdentityDbContext {

        public DbSet<Item> Items { get; set; }
        public DbSet<ShopUser> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemDetails> ItemDetails { get; set; }

        public Context(DbContextOptions options) : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Item>();

            builder.Entity<ItemDetails>();

            builder.Entity<ShopUser>();

            builder.Entity<Order>().HasOne(t => t.User).WithMany();

            builder.Entity<OrderItem>().HasKey(s => new { s.OrderId, s.ItemId });

           // builder.Entity<Item>().HasData(
           //     new Item { Id = 1, Count = 50, Name = "Water", Price = 1.5, Type = "Beverage" },
           // new Item { Id = 2, Count = 50, Name = "Cola", Price = 1.5, Type = "Beverage" },
           // new Item { Id = 3, Count = 50, Name = "Cola Light", Price = 1.5, Type = "Beverage" },
           // new Item { Id = 4, Count = 50, Name = "Cola Zero", Price = 1.5, Type = "Beverage" },
           // new Item { Id = 5, Count = 50, Name = "Ice Tea", Price = 1.5, Type = "Beverage" },
           // new Item { Id = 6, Count = 50, Name = "Hot Dog", Price = 2F, Type = "Food" },
           // new Item { Id = 7, Count = 50, Name = "Pizza Margherita", Price = 3F, Type = "Food" },
           // new Item { Id = 8, Count = 50, Name = "Pizza Bolognese", Price = 3F, Type = "Food" },
           // new Item { Id = 9, Count = 50, Name = "Pizza Four Cheese", Price = 3F, Type = "Food" },
           // new Item { Id = 10, Count = 50, Name = "Pizza Bolognese", Price = 3F, Type = "Food" }
           //);

           // builder.Entity<ItemDetails>().HasData(
           //         new ItemDetails { Id = 1, ItemId = 1, Allergens = "None", Calories = 0D, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 2, ItemId = 2, Allergens = "None", Calories = 90D, SaltContent = 0D, Size = "200ml", SugarContent = 21.4 },
           //         new ItemDetails { Id = 3, ItemId = 3, Allergens = "None", Calories = 0.8, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 4, ItemId = 4, Allergens = "None", Calories = 0.6, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 5, ItemId = 5, Allergens = "None", Calories = 0.6, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 6, ItemId = 6, Allergens = "None", Calories = 0.6, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 7, ItemId = 7, Allergens = "None", Calories = 0.6, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 8, ItemId = 8, Allergens = "None", Calories = 0.6, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 9, ItemId = 9, Allergens = "None", Calories = 0.6, SaltContent = 0D, Size = "200ml", SugarContent = 0D },
           //         new ItemDetails { Id = 10, ItemId = 10, Allergens = "None", Calories = 0.6, SaltContent = 0D, Size = "200ml", SugarContent = 0D }
           // );
        }
    }
}
