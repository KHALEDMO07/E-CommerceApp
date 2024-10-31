using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public Cart cart { get; set; }

        public decimal TotalPrice { get; set; } 
    }
} 
