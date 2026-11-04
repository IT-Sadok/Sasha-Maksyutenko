using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IValidator<RegisterDTO> _registerValidator;
        private readonly IValidator<LoginDTO> _loginValidator;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IValidator<LoginDTO> loginValidator, IValidator<RegisterDTO> registerValidator)
        {
            _authService = authService;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator; 
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
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
            var validationResult = await _loginValidator.ValidateAsync(loginDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
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
