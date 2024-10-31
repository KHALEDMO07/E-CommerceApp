using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {

        [Key]
        public int Id { get; set; }

        [Required , MaxLength(255)]
        public string Name { get; set; }

        [Required , MaxLength(255)]
        public string Description { get; set; }

        public decimal price { get; set; }  

        public byte[] image { get; set; }

        public int CategoryId { get; set; }

        public Category category { get; set; } = new();

        public List<OrderItem> ?OrderItems { get; set; }

        public List<CartProduct>? CartProducts { get; set; }

       public List<WishlistProduct>? WishlistProducts { get;set; }


             
    }
}
