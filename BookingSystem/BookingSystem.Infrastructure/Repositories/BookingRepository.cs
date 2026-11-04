

using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        public BookingRepository(AppDbContext context) => _context = context;
        public async Task<Booking> CreateBooking(Booking booking)
        {
            booking.Id = Guid.NewGuid();
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(string userId)
        {
            return await _context.Bookings
                .Include(b => b.Apartment)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> IsBookingAvailable(Guid apartmentId, DateTime startDate, DateTime endDate)
        {
            return !await _context.Bookings.AnyAsync(b =>b.ApartmentId == apartmentId && b.StartDate < endDate && b.EndDate > startDate);
        }
    }
}
