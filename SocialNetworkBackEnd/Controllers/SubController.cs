using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Interafaces.Sub;
using SocialNetworkBackEnd.Models.Sub.ViewModels;
using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class SubController : Controller
    {
        private readonly ISubService _subService;
        public SubController(ISubService subService)
        {
            _subService = subService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult Subscribe([FromBody] SubBlank result)
        {
            Guid userID = Guid.Parse(User.Identity.Name);
            if (userID == result.SubPerson) return BadRequest("Вы не можете подписаться на самого себя");
            return _subService.SubAction(userID, result.SubPerson) ? Ok() : BadRequest();
        }

        [HttpGet("{id}/followers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult ShowFollowers(Guid id)
        {
            IEnumerable<UserMiniView> result = _subService.GetFollowers(id);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpGet("{id}/subscribers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult ShowSubscribers(Guid id)
        {
            IEnumerable<UserMiniView> result = _subService.GetSubscribers(id);
            return result != null ? Ok(result) : BadRequest();
        }            

    }
}