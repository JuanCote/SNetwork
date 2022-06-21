using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models.User.ViewModels
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserLogin() { }
    }
}
