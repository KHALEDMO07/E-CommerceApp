using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ///the One-To-Many Relationship between the Category and product 
            builder.HasOne(p => p.category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId); 

            ///The One-To-Many Relationship between The Product and OrderItems   
            
            builder.HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);
            ///The One-To-Many Relationship between The Product and CartProduct
            
            builder.HasMany(p => p.CartProducts)
                .WithOne(cp => cp.Product)
                .HasForeignKey(cp => cp.ProductId);

          

            ///The One-To-Many Relationship between The Product and WishlistProduct
            
            builder.HasMany(p => p.WishlistProducts)
                .WithOne(w=>w.product)
                .HasForeignKey(c => c.ProductId);

        }
    }
}
