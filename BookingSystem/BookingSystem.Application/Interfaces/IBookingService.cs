

using BookingSystem.Application.DTOs;

namespace BookingSystem.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDTO> CreateBookingAsync(CreateBookingDTO bookingDto, string userId);
        Task<PagedResult<BookingDTO>> GetBookingsByUserIdAsync(string userId, int page, int pageSize);
    }
}
