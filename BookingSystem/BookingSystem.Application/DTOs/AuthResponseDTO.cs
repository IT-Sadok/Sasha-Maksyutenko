
namespace BookingSystem.Application.DTOs
{
    public record AuthResponseDTO(string JwtToken, Guid UserId, string Role);
}
