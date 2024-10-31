using E_Commerce.Enum;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class CreatePaymentDto
    {
       

        [Required]
        public DelevieryMethod PaymentMethod { get; set; }   
    }
}
