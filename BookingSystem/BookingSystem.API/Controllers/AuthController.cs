using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try {
                var result = await _authService.RegisterAsync(registerDTO);
                return Ok(result);
            }
            catch(Exception ex) {
                return BadRequest($"{ex.Message}, Registration failed");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try {
                var result = await _authService.LoginAsync(loginDTO);
                return Ok(result);
            }
            catch(Exception ex) {
                return BadRequest($"{ex.Message}, Login failed");
            }
        }
    }
}
