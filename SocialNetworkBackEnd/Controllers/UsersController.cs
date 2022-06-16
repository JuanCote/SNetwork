using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SocialNetworkBackEnd.Interafaces;
using SocialNetworkBackEnd.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authorization;
using SocialNetworkBackEnd.Models.User.ViewModels;

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
        public IEnumerable<UserMiniView> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetUserById(Guid id)
        {
            UserView result = _userService.GetUserById(id);

            return result == null ? BadRequest() : Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddUser([FromBody] UserBlank user)
        {   
            return _userService.AddUser(user) ? Ok() : BadRequest();
        }

        [HttpPost("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult DeleteUser(Guid id)
        {
            return _userService.DeleteUser(id) ? Ok(id) : NoContent();
        }

        [HttpPost("edit/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult EditUser([FromBody] UserBlankEdit user, Guid id)
        {
            return _userService.EditUser(user,id) ? Ok() : BadRequest();
        }

    }
}
