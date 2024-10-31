namespace E_Commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User user { get; set; } 
        public decimal TotalPrice { get; set; }

        public List<CartProduct>?CartProducts { get; set; }  
    }
}
