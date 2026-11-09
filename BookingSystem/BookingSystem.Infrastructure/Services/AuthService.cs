using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingSystem.Application.Options;

namespace BookingSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<JwtOptions> _jwtOptions;
        public AuthService(UserManager<ApplicationUser> userManager,IOptions<JwtOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtOptions = jwtOptions;
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
           if(user==null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password)) throw new Exception("Invalid credentials");
            return new AuthResponseDTO
            {
                JwtToken = GenerateJwtToken(user),
                UserId = Guid.Parse(user.Id),
                Role = user.Role,
            };
        }
        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                Name = registerDTO.Name,
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                Role = registerDTO.Role
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if(!result.Succeeded) throw new Exception(string.Join(",",result.Errors.Select(e => e.Description)));
            return new AuthResponseDTO
            {
                JwtToken = GenerateJwtToken(user),
                UserId = Guid.Parse(user.Id),
                Role = user.Role,
            };
        }
        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token =new JwtSecurityToken(
                expires:DateTime.UtcNow.AddHours(_jwtOptions.Value.ExpirationHours),
                claims: claims,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
