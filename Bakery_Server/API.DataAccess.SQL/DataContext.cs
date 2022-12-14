using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<BakeryOrder> BakeryOrders { get; set; }
        public DbSet<BakeryOrder> OrderItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Basket>()
                .HasMany(x => x.basketItems)
                .WithOne(x => x.basket);

            builder.Entity<BasketItem>()
                .HasOne(x => x.product);

            builder.Entity<Product>()
                .HasMany(x => x.productImages)
                .WithOne(x => x.product);

            builder.Entity<BakeryOrder>()
                .HasMany(x => x.orderItems)
                .WithOne(x => x.parentOrder);
        }
    }
}
