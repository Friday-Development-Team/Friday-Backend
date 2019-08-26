using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data {
    public class Context : DbContext {

        public DbSet<Item> items;
        public DbSet<ShopUser> users;
        public DbSet<Order> orders;

        public Context(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Item>();

            builder.Entity<ShopUser>();

            builder.Entity<Order>();

            builder.Entity<OrderItem>().HasKey(s => new { s.OrderId, s.ItemId });

            builder.Entity<Item>().HasData(
                new Item { Id = 1, Count = 50, Name = "Water", Price = 1.5, Type = "Beverage" },
            new Item { Id = 2, Count = 50, Name = "Cola", Price = 1.5, Type = "Beverage" },
            new Item { Id = 3, Count = 50, Name = "Cola Light", Price = 1.5, Type = "Beverage" },
            new Item { Id = 4, Count = 50, Name = "Cola Zero", Price = 1.5, Type = "Beverage" },
            new Item { Id = 5, Count = 50, Name = "Ice Tea", Price = 1.5, Type = "Beverage" },
            new Item { Id = 6, Count = 50, Name = "Hot Dog", Price = 2F, Type = "Food" },
            new Item { Id = 7, Count = 50, Name = "Pizza Margherita", Price = 3F, Type = "Food" },
            new Item { Id = 8, Count = 50, Name = "Pizza Bolognese", Price = 3F, Type = "Food" },
            new Item { Id = 9, Count = 50, Name = "Pizza Four Cheese", Price = 3F, Type = "Food" },
            new Item { Id = 10, Count = 50, Name = "Pizza Bolognese", Price = 3F, Type = "Food" }
           );
        }
    }
}
