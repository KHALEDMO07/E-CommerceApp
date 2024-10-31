using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Data.Configurations
{
    public class ShipmentPriceConfiguration : IEntityTypeConfiguration<ShipmentPrice>
    {
        public void Configure(EntityTypeBuilder<ShipmentPrice> builder)
        {
            builder.HasKey(sp => new { sp.country, sp.city, sp.region }); 
        }
    }
}
