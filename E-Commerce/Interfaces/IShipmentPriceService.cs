using E_Commerce.DTOs;
using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IShipmentPriceService
    {
        Task<ShipmentPrice> AddAsync(ShipmentPrice shipmentPrice);   
        IEnumerable<ShipmentPrice> GetAll();

        ShipmentPrice GetOne(AddressDto dto);
        ShipmentPrice Update(ShipmentPrice shipmentPrice);

        ShipmentPrice Delete(ShipmentPrice shipmentPrice);
    }
}
