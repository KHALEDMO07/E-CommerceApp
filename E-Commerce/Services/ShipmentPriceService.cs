using E_Commerce.Data;
using E_Commerce.DTOs;
using E_Commerce.Interfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class ShipmentPriceService(AppDbContext _context) : IShipmentPriceService
    {
        public async Task<ShipmentPrice> AddAsync(ShipmentPrice shipmentPrice)
        {
            await _context.ShipmentPrice.AddAsync(shipmentPrice);   
            return shipmentPrice;
        }

        public ShipmentPrice Delete(ShipmentPrice shipmentPrice)
        {
            _context.ShipmentPrice.Remove(shipmentPrice);
            return shipmentPrice;   
        }

        public IEnumerable<ShipmentPrice> GetAll()
        {
            var res = _context.ShipmentPrice.ToList(); 
            return res;
        }

        public ShipmentPrice GetOne(AddressDto dto)
        {
            var shipment = _context.ShipmentPrice.FirstOrDefault(sp => sp.country == dto.country && 
            sp.city == dto.city && sp.region == dto.region);

            return shipment;
        }

        public ShipmentPrice Update(ShipmentPrice shipmentPrice)
        {
           _context.Update(shipmentPrice);
            return shipmentPrice;
        }
    }
}
