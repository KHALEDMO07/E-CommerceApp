using E_Commerce.Data;
using E_Commerce.Interfaces;
using E_Commerce.Models;

namespace E_Commerce.Services
{
    public class ShipmentService(AppDbContext _context) : IShipmentService
    {
        public async Task<Shipment> AddAsync(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            return shipment;
        }

        public Shipment Delete(Shipment shipment)
        {
            _context.Shipments.Remove(shipment);
            return shipment;
        }

        public IEnumerable<Shipment> GetAll()
        {
            var shipmentList = _context.Shipments.ToList(); 
            return shipmentList;
        }

        public Shipment GetShipmentById(int shipmentId)
        {
            var shipment = _context.Shipments.FirstOrDefault(s => s.Id == shipmentId);

            return shipment;
        }

        public Shipment Update(Shipment shipment)
        {
            _context.Update(shipment);
            return shipment;
        }
    }
}
