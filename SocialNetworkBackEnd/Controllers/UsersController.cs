using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authorization;
using SocialNetworkBackEnd.Models.User.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Primitives;

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

        [HttpPost("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public ActionResult DeleteUser(Guid id)
        {
            return _userService.DeleteUser(id) ? Ok(id) : NoContent();
        }

        [HttpPost("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public ActionResult EditUser([FromBody] UserBlankEdit user, Guid id)
        {
            return _userService.EditUser(user,id) ? Ok() : BadRequest();
        }

    }
}
