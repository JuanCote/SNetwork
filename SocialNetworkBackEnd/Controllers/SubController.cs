using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Interafaces.Sub;
using SocialNetworkBackEnd.Models.Sub.ViewModels;
using System;

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
        public ActionResult Subscribe([FromBody] SubBlank result )
        {
            Guid userID = Guid.Parse(User.Identity.Name);
            if (userID == result.Id) return BadRequest("Вы не можете подписаться на самого себя");
            return _subService.Subscribe(userID, result.Id) ? Ok() : BadRequest();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult Unsubscribe([FromBody] SubBlank result)
        {
            
        }
    }
}
