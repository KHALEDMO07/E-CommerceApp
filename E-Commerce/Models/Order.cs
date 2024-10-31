namespace E_Commerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime PlacedOn { get; set; }

        public Decimal TotalPrice { get; set; } 

        public int UserId { get; set; } 

        public User user { get; set; }

        public List<OrderItem> Items { get; set; } 

        public int? PaymentId { get; set; }  

        public Payment? Payment { get; set; }    

       public int? ShipmentId { get; set; } 

        public Shipment? shipment { get; set; }

        public bool IsConfirmed { get; set; }       
    }
}
