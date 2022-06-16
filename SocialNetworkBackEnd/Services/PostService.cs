using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.Post;
using SocialNetworkBackEnd.Models.Post.ViewModels;
using SocialNetworkBackEnd.Models.User.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkBackEnd.Services
{
    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public IEnumerable<PostView> GetPosts(Guid id)
        {
            IEnumerable<PostDB> posts = _postRepository.GetPosts(id, false);
            return posts.Select(ConvertingPostModels.FromPostDBToPostDomain)
                        .Select(ConvertingPostModels.FromPostDomainToPostView)
                        .OrderBy(p => p.CreationDate);
        }
        public PostView AddPost(PostBlank post)
        {
            string textForCheck = post.Text.Replace("<p>", "").Replace("</p>", "");
            Console.WriteLine(textForCheck);
            if (post.Text == "<p><br></p>" || string.IsNullOrWhiteSpace(textForCheck) || textForCheck == "") 
            {
                return null;
            }
            PostDomain postDm = ConvertingPostModels.FromPostBlankToPostDomain(post);
            bool result = _postRepository.AddPost(
                    ConvertingPostModels.FromPostDomainToPostDB(postDm));
            if (!result) return null;
            var addedPost = ConvertingPostModels.FromPostDomainToPostView(
                            ConvertingPostModels.FromPostDBToPostDomain(_postRepository.getPostById(postDm.Id))
                            );
            return addedPost;
        }
        public Guid? deletePost(Guid id)
        {
            return _postRepository.deletePost(id);
        }

        public string FindUser(UserLogin user)
        {
            return "email not found";
        }
    }
}
