using E_Commerce.Models;

namespace E_Commerce.Interfaces
{
    public interface IShipmentService
    {
        Task<Shipment>AddAsync(Shipment shipment);
        IEnumerable<Shipment> GetAll(); 

        Shipment GetShipmentById(int shipmentId);

        Shipment Update(Shipment shipment); 
        Shipment Delete(Shipment shipment);
    }
}
