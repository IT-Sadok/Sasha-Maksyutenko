
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
        public async Task<Booking?> CreateBooking(Booking booking)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

            var isAvailable = !await _context.Bookings.AnyAsync(b =>
                b.ApartmentId == booking.ApartmentId &&
                b.StartDate < booking.EndDate &&
                b.EndDate > booking.StartDate);

            if (!isAvailable)
            {
                await transaction.RollbackAsync();
                return null;
            }

            booking.Id = Guid.NewGuid();
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return booking;
        }

        public async Task<(IEnumerable<Booking> Items, int TotalCount)> GetBookingsByUserId(string userId, int page, int pageSize)
        {
            var query = _context.Bookings
                .Include(b => b.Apartment)
                .Where(b => b.UserId == userId);
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, totalCount);
        }
    }
}
