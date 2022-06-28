using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models.User.ViewModels
{
    public class UserBlankEdit
    {
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string? Avatar { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public UserBlankEdit() { }
    }
}
