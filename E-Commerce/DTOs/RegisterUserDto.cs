using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Your Name Is Required"), MaxLength(255)]


        public string FullName { get; set; }


        [Required(ErrorMessage = "Your UserName Is Required"), MaxLength(255)]
        public string UserName { get; set; }


        [Required]

        [EmailAddress(ErrorMessage = "Enter Correct Email Please!")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{6,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [RegularExpression("^(Admin|Customer)$", ErrorMessage = "The type must be either Admin Or Customer.")]
        public string Role { get; set; }
        public string Country { get; set; }

        public string City { get; set; }

        public string Region { get; set; }
    }
}
