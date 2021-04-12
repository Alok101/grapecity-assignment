using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuth.Business.Contract.Interface;
using OAuth.Business.Contract.Models;
using OAuth.Business.Service.Interfaces;

namespace OAuth.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly ITokenRefresher tokenRefresher;
        private readonly IUserService userService;
        public TokenController(IJwtAuthenticationManager jwtAuthenticationManager, ITokenRefresher tokenRefresher, IUserService userService)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.tokenRefresher = tokenRefresher;
            this.userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserViewModel userCred)
        {
            var token = jwtAuthenticationManager.Authenticate(userCred.UserName, userCred.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshCred refreshCred)
        {
            var token = tokenRefresher.Refresh(refreshCred);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("adduser")]
        public IActionResult AddNewUser([FromBody] UserViewModel userModel)
        {
            var username = userService.AddNewUser(userModel);
            return Ok(username);
        }
    }
}
