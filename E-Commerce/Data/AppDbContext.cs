﻿using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<User>Users { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }

        public DbSet<Product>Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }    
        public DbSet<Category> Categories { get; set; }

        public DbSet<Payment> Payments { get; set; } 

        public DbSet<Shipment>Shipments { get; set; }

        public DbSet<Cart>Carts { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<WishlistProduct> WishlistProduct { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        public DbSet<ShipmentPrice> ShipmentPrice{ get; set; }    
        public AppDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
