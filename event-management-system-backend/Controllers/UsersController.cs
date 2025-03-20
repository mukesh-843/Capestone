using Microsoft.AspNetCore.Mvc;
using event_management_system_backend.DTOs;
using event_management_system_backend.Service;

namespace event_management_system_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AuthService _authService;

        public UsersController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                var user = await _authService.Register(userDTO);
                var token = _authService.GenerateJWT(user);
                return Ok(new { user, token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _authService.Authenticate(loginDTO);
            if (user == null) return Unauthorized();

            var token = _authService.GenerateJWT(user);
            return Ok(new { user, token });
        }
    }
}
