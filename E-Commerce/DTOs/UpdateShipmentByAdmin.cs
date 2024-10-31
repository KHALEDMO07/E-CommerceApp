using E_Commerce.Enum;

namespace E_Commerce.DTOs
{
    public class UpdateShipmentByAdmin
    {
        public int shipmentId {  get; set; }    
        public ShipmentStatus status { get; set; }
    }
}
