using SocialNetworkBackEnd.Models.Post;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Interafaces.Post
{
    public interface IPostRepository
    {
        IEnumerable<PostDB> GetPosts(Guid id, bool showIsDeleted);
        bool AddPost(PostDB post);
        PostDB getPostById(Guid id);
        Guid? deletePost(Guid id);
    }
}
