using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;
using Friday.Models.Logs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data
{
    /// <summary>
    /// Declares a DBContext. Used to map and declare the DbSets and the mapping for each class.
    /// </summary>
    public class Context : IdentityDbContext
    {
        /// <summary>
        /// DB data for Items. Can include ItemDetails.
        /// </summary>
        public DbSet<Item> Items { get; set; }
        /// <summary>
        /// DB data for ShopUsers. Can include Orders and CurrencyLogs.
        /// </summary>
        public DbSet<ShopUser> ShopUsers { get; set; }
        /// <summary>
        /// DB data for Orders. Can include OrderItems and ShopUser.
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        /// <summary>
        /// DB data for ItemDetails.
        /// </summary>
        public DbSet<ItemDetails> ItemDetails { get; set; }
        /// <summary>
        /// DB data for Configuration.
        /// </summary>
        public DbSet<Configuration> Configuration { get; set; }
        /// <summary>
        /// DB data for CurrencyLogs. Can Include ShopUser.
        /// </summary>
        public DbSet<CurrencyLog> CurrencyLogs { get; set; }
        /// <summary>
        /// DB data for ItemLogs. Can include Item and ShopUser.
        /// </summary>
        public DbSet<ItemLog> ItemLogs { get; set; }
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="options">Options for DB</param>
        public Context(DbContextOptions options) : base(options)
        {

        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>();

            builder.Entity<ItemDetails>();

            builder.Entity<ShopUser>();

            builder.Entity<Order>().HasOne(t => t.User).WithMany(t => t.Orders);

            builder.Entity<OrderItem>().HasKey(s => new { s.OrderId, s.ItemId });

            builder.Entity<Configuration>();

            builder.Entity<CurrencyLog>();

        }
    }
}
