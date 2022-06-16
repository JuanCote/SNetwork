﻿using System.ComponentModel.DataAnnotations;

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
        public string Email { get; set; }
        public string Password { get; set; }
        //public EditUserBlank(string name,
        //                 string surname,
        //                 int? age,
        //                 string? avatar,
        //                 string? description,
        //                 string? status,
        //                 string email,
        //                 string password)
        //{
        //    Name = name;
        //    Surname = surname;
        //    Age = age;
        //    Avatar = avatar;
        //    Description = description;
        //    Status = status;
        //    Email = email;
        //    Password = password;
        //}
        public UserBlankEdit() { } //TODO: спросить почему выбрасывает 500 ошибку без пустого конструктора
    }
}