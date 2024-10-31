using E_Commerce.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class ProductDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public Byte[] imageFile { get; set; }

        [Required]
        public string categoryName { get; set; }
    }
}
