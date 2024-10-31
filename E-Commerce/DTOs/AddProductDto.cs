using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class AddProductDto
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string categoryName { get; set; }



    }
}
