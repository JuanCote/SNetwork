using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackEnd.Models.User.ViewModels;

namespace SocialNetworkBackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Login([FromBody] UserLogin user)
        {
            var response = new
            {
                username = user.Email,
                password = user.Password,
                remember_me = user.RememberMe,
            };
            return Ok(response);
        }
    }
}
