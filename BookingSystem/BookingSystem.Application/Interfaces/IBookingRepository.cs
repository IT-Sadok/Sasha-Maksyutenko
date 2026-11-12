
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking>CreateBooking(Booking booking);
        Task <(IEnumerable<Booking>Items, int TotalCount)> GetBookingsByUserId(string userId, int page, int pageSize);
        Task<bool> IsBookingAvailable(Guid apartmentId, DateTime startDate, DateTime endDate);
    }
}
