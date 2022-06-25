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
        public Guid PostOwner { get; set; }
        public PostDomain(Guid id, Guid userId, string text, DateTime creationDate, bool isDeleted, Guid postOwner) 
            : this(id, userId, text, creationDate, isDeleted, postOwner, null, null, null) { }
        public PostDomain(Guid id, Guid userId, string text, DateTime creationDate, bool isDeleted, Guid postOwner, string name, string surname, string avatar)
        {
            Id = id;
            UserId = userId;
            Text = text;
            CreationDate = creationDate;
            IsDeleted = isDeleted;
            Name = name;
            Surname = surname;
            Avatar = avatar;
            PostOwner = postOwner;
        }

    }
}
