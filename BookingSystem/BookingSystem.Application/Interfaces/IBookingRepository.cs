

using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking>CreateBooking(Booking booking);
        Task <IEnumerable<Booking>> GetBookingsByUserId(string userId);
        Task<bool> IsBookingAvailable(Guid apartmentId, DateTime startDate, DateTime endDate);
    }
}
