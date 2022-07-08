using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialNetworkBackEnd.Interafaces.User;
using SocialNetworkBackEnd.Models.User.ViewModels;
using SocialNetworkBackEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialNetworkBackEnd.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Login([FromBody] UserLogin user)
        {
            LoginResult result = _userService.Login(user);

            if (result.status != Constants.GOOD) return BadRequest(result.status);

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, result.user.Id.ToString()) };

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(
                new
                {
                    access_token = encodedJwt,
                    user = result.user,
                    isAdmin = _userService.AdminCheck(result.user.Id)
                }
            );
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult AuthMe()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            UserView user = _userService.GetUserById(Guid.Parse(User.Identity.Name), null);
            return Ok(
                new
                {
                    user = user,
                    isAdmin = _userService.AdminCheck(user.Id)
                }
            );
        }

    }
}
