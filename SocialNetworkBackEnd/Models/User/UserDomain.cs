using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkBackEnd.Models.User
{
    public class UserDomain
    {
        [Required(ErrorMessage = "Id is required!")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required!")]
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public UserDomain(Guid id, string name, string surname, int? age,
            string avatar, string description, string status,
            bool isDeleted, string email, string password)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            Avatar = avatar;
            Description = description;
            Status = status;
            IsDeleted = isDeleted;
            Email = email;
            Password = password;
        }
    }
}
