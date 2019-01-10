using Auth.API.Models;
using Auth.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// Checks if a user is logged in.
        /// </summary>
        [HttpGet("loggedIn/{token}")]
        public ActionResult LoggedIn(string token)
        {
            if (_authRepository.IsLoggedIn(token))
            {
                return Ok();
            }

            return NotFound();
            
        }

        /// <summary>
        /// Logins a user.
        /// </summary>
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest();
            }

            var token = _authRepository.Login(loginRequest);
            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }

        /// <summary>
        /// Disconnects a user.
        /// </summary>
        [HttpPost("logout")]
        public ActionResult<string> Logout([FromBody] LogoutRequest logoutRequest)
        {
            if (logoutRequest == null)
            {
                return BadRequest();
            }

            _authRepository.Logout(logoutRequest);
         
            return Ok();
        }
    }
}
