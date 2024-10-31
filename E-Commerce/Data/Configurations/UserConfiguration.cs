using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.UserName)
                .IsUnique();

            ///The One-To-Many Relationship between User(Customer) and Order
            builder.HasMany(u => u.orders)
                .WithOne(o => o.user)
                .HasForeignKey(o => o.UserId);

            ///The One-To-Many Relationship between User(Customer) and Wishlist
            
            builder.HasMany(u => u.wishlists)
                .WithOne(w => w.user)
                .HasForeignKey(w => w.UserId);

            ///The One-To-Many Relationship between User(Customer) and Cart 
            
            builder.HasMany(u => u.Carts)
                .WithOne(c => c.user)
                .HasForeignKey(c => c.UserId);

             ///The One-To-Many Relationship between User(Customer) and Shipment 
             
            builder.HasMany(u => u.shipments)
                .WithOne(sh => sh.user)
                .HasForeignKey(sh => sh.UserId);
            ///The One-To-Many Relationship between User(Customer) and Payments
            
            builder.HasMany(u => u.payments)
                .WithOne(p=>p.user)
                .HasForeignKey(p => p.UserId);
        }
    }
}
