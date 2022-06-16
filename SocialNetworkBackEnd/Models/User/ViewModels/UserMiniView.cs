using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models.ViewModels
{
    public class UserMiniView
    {
        [Required(ErrorMessage = "Id is required!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string? Avatar { get; set; }
        public string? Status { get; set; }
        public UserMiniView(Guid id, string name, string surname,int? age, string? avatar, string? status)
        {
            Id = id;
            Name = name;
            Age = age;
            Surname = surname;
            Avatar = avatar;
            Status = status;
        }
    }
}
