using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models.User.ViewModels
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Электронная почта должна быть заполненна")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Электронная почта должна быть заполненна")]
        public string Password { get; set; }
        public UserLogin() { }
    }
}
