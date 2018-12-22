using System.Collections.Generic;
using Auth.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("loggedIn/{token}")]
        public ActionResult LoggedIn(string token)
        {
            if (_authService.IsLoggedIn(token))
            {
                return Ok();
            }

            return NotFound();
            
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] string email, string password)
        {
            string token = _authService.Login(email, password);
            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }
    }
}
