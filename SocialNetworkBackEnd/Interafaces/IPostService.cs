using SocialNetworkBackEnd.Models.Post.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Interafaces
{
    public interface IPostService
    {
        IEnumerable<PostView> GetPosts(Guid id);
        PostView AddPost(PostBlank post);
        Guid? deletePost(Guid id);
    }
}
