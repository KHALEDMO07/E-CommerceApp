using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Your UserName Is Required"), MaxLength(255)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Is Required!")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{6,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }    
    }
}
