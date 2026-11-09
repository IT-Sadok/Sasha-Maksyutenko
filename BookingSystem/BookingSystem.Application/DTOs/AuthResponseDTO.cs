using BookingSystem.Domain.Enums;

namespace BookingSystem.Application.DTOs
{
    public record AuthResponseDTO(string JwtToken, Guid UserId, Role Role);
}
