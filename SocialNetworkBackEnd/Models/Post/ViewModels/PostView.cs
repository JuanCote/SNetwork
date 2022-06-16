
using System;

namespace SocialNetworkBackEnd.Models.Post.ViewModels
{
    public class PostView
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string? Avatar { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreationDate { get; set; }
        public PostView(Guid id, string text, string avatar, string name, string surname, DateTime creationDate)
        {
            Id = id;
            Text = text;
            Avatar = avatar;
            Name = name;
            Surname = surname;
            CreationDate = creationDate;
        }
    }
}
