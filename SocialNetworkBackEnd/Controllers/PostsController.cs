using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.Post.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class PostsController : Controller
    {
        IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IEnumerable<PostView> GetPosts(Guid id)
        {
            return _postService.GetPosts(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddPost([FromBody] PostBlank post)
        {
            PostView result = _postService.AddPost(post);
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpPost("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult DeletePost(Guid id)
        {
            return _postService.deletePost(id) == id ? Ok(id) : NoContent(); 
        }


    }
}
