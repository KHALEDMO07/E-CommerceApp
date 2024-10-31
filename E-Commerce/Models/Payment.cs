using E_Commerce.Enum;

namespace E_Commerce.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DelevieryMethod PaymentMethod { get; set; }

        public decimal PaidAmount { get; set; }

        public List<Order> Orders { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }  
    }
}
