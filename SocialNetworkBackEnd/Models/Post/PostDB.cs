using System;

namespace SocialNetworkBackEnd.Models.Post
{
    public class PostDB
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Guid PostOwner { get; set; }
        public PostDB(Guid id,
                      Guid userId,
                      string text,
                      DateTime creationDate,
                      DateTime? modifiedDate,
                      bool isDeleted,
                      Guid postOwner,
                      string? avatar,
                      string? name,
                      string? surname
            )
        {
            Id = id;
            UserId = userId;
            Text = text;
            CreationDate = creationDate;
            ModifiedDate = modifiedDate;
            IsDeleted = isDeleted;
            Avatar = avatar;
            Name = name;
            Surname = surname;
            PostOwner = postOwner;
        }
    }
}
