using SocialNetworkBackEnd.Models.Post.ViewModels;
using System;

namespace SocialNetworkBackEnd.Models.Post
{
    /// <summary>
    /// Конвертация моделей постов.
    /// </summary>
    public static class ConvertingPostModels
    {
        public static PostDomain FromPostDBToPostDomain(PostDB post)
        {
            return new PostDomain(
                post.Id,
                post.UserId,
                post.Text,
                post.CreationDate,
                post.IsDeleted,
                post.Name,
                post.Surname,
                post.Avatar
                );
        }
        public static PostView FromPostDomainToPostView(PostDomain post)
        {
            return new PostView(
                post.Id,
                post.Text,
                post.Avatar,
                post.Name,
                post.Surname,
                post.CreationDate
                );
        }
        public static PostDomain FromPostBlankToPostDomain(PostBlank post)
        {
            return new PostDomain(
                Guid.NewGuid(),
                post.UserId,
                post.Text,
                DateTime.Now,
                false
                );
        }
        public static PostDB FromPostDomainToPostDB(PostDomain post)
        {
            return new PostDB(
                post.Id,
                post.UserId,
                post.Text,
                post.CreationDate,
                null,
                post.IsDeleted,
                post.Avatar,
                post.Name,
                post.Surname
                ) ;

        }
    }
}
