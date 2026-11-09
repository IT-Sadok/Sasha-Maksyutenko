using BookingSystem.Domain.Enums;

namespace BookingSystem.Application.DTOs
{
    public record RegisterDTO(string Name, string Email, string Password, Role Role);
}
