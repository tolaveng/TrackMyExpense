using System.ComponentModel.DataAnnotations;

namespace Core.Application.Models
{
    public class UserDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [StringLength(32, ErrorMessage = "Email too long (32 character limit)")]
        public string Email { get; set; }

        [EmailAddress]
        [StringLength(32, ErrorMessage = "Email too long (32 character limit)")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "Password too long (32 character limit)")]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}