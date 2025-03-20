using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using event_management_system_backend.DTOs;
using event_management_system_backend.Service;

namespace event_management_system_backend.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AuthService _authService;

        public AdminController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin(UserDTO userDTO)
        {
            try
            {
                var user = await _authService.Register(userDTO, "Admin");
                var token = _authService.GenerateJWT(user);
                return Ok(new { user, token});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
