using BookingSystem.Domain.Enums;

namespace BookingSystem.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string JwtToken { get; set; }
        public Guid UserId { get; set; }
        public Role Role { get; set; }
    }
}
