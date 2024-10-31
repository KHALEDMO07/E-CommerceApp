using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ///the One-To-Many Relationship between The Order and OrderItems 

            builder.HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId); 

            ///the One-To-Many Relationship between Payment and Order 
            
            builder.HasOne(o => o.Payment)
                .WithMany(pay => pay.Orders)
                .HasForeignKey(o => o.PaymentId);

            ///the One-To-Many Relationship between Shipment and Order
            
            builder.HasOne(o=>o.shipment)
                .WithMany(sh => sh.Orders)
                .HasForeignKey(o=>o.ShipmentId);    
        }
    }
}
