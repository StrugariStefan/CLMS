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

        [HttpGet("loggedIn/{token}")]
        public ActionResult LoggedIn(string token)
        {
            if (_authRepository.IsLoggedIn(token))
            {
                return Ok();
            }

            return NotFound();
            
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginRequest loginRequest)
        {
            string token = _authRepository.Login(loginRequest);
            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }
    }
}
