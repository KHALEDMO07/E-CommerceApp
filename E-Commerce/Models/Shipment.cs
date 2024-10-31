using E_Commerce.Enum;
using System.Data;

namespace E_Commerce.Models
{
    public class Shipment
    {
        public int Id { get; set; }

        public DateTime ShipmentTime { get; set; } // The date when the shipment created by customer 

        public ShipmentStatus Status { get; set; }

        public DateTime? DeliveredOn { get; set; } 

        public List<Order> Orders { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }

        public decimal shipmentPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
