using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Your Name Is Required"),MaxLength(255)]


        public string FullName { get; set; }


        [Required(ErrorMessage = "Your UserName Is Required"), MaxLength(255)]
        public string UserName {  get; set; }


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

        public string verifyCode { get; set; }

        public bool IsActive { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public UserAddress address { get; set; }
        public List<Order>?orders { get; set; }

        public List<Cart>?Carts { get; set; }

        public List<Wishlist>? wishlists { get; set; }

        public List<Shipment>?shipments { get; set; }

        public List<Payment>?payments { get; set; }
    }
}
