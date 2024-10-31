using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class UpdateUserNameDto
    {
        [Required , MaxLength(255)]
        public string UserName { get; set; }

    }
}
