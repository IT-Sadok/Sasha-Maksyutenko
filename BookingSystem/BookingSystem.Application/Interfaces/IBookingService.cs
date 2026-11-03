

using BookingSystem.Application.DTOs;

namespace BookingSystem.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDTO> CreateBookingAsync(CreateBookingDTO bookingDto);
        Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(string userId);
        Task<bool> IsBookingAvailable(Guid apartmentId, DateTime startDate, DateTime endDate);
    }
}
