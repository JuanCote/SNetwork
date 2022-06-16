using System;

namespace SocialNetworkBackEnd.Models.Post
{
    public class PostDomain
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Avatar { get; set; }
        public PostDomain(Guid id, Guid userId, string text, DateTime creationDate, bool isDeleted) 
            : this(id, userId, text, creationDate, isDeleted, null, null, null) { }
        public PostDomain(Guid id, Guid userId, string text, DateTime creationDate, bool isDeleted, string name, string surname, string avatar)
        {
            Id = id;
            UserId = userId;
            Text = text;
            CreationDate = creationDate;
            IsDeleted = isDeleted;
            Name = name;
            Surname = surname;
            Avatar = avatar;
        }

    }
}
