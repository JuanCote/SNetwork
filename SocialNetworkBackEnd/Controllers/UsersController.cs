using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.User.ViewModels;
using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkBackEnd.Controllers
{
    
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public IEnumerable<UserMiniView> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public ActionResult GetUserById(Guid id)
        {
            UserView result = _userService.GetUserById(id);
            string email = HttpContext.User.Identity.Name;
            Console.WriteLine(email);
            return result == null ? BadRequest() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddUser([FromBody] UserBlank user)
        {
            string result = _userService.AddUser(user);
            return result == Constants.GOOD ? Ok() : BadRequest(result);
        }
        public class JsonId
        {
            public Guid id;
        }
        [HttpPost("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult DeleteUser([FromBody] JsonId reqId)
        {
            Guid requestUserId = Guid.Parse(User.Identity.Name);
            bool isAdmin = _userService.AdminCheck(requestUserId);
            if (reqId.id != requestUserId && isAdmin == false)
            {
                return BadRequest("Обычный пользователь не может удалять других пользователей");
            }
            return _userService.DeleteUser(reqId.id) ? Ok(reqId.id) : BadRequest();
        }

        [HttpPost("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult EditUser([FromBody] UserBlankEdit user, Guid id)
        {
            return _userService.EditUser(user, id) ? Ok() : BadRequest();
        }
    }
    
}
