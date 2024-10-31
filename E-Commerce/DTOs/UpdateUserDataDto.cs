using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class UpdateUserDataDto
    {
        [Required(ErrorMessage = "Your Name Is Required"), MaxLength(255)]


        public string FullName { get; set; }

        [RegularExpression("^(Admin|Customer)$", ErrorMessage = "The type must be either Admin Or Customer.")]
        public string Role { get; set; }

        [EmailAddress(ErrorMessage = "Enter Correct Email Please!")]

        public string Email { get; set; }
        public string Country { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

    }
}
