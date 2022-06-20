using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models.ViewModels
{
    public class UserBlank
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        public UserBlank() { } 
    }
}
