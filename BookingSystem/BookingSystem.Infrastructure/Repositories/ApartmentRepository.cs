
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _context;
        public ApartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Apartment> AddApartment(Apartment apartment)
        {
            apartment.Id = Guid.NewGuid();
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }

        public async Task<(IEnumerable<Apartment> Items, int TotalCount)> GetAll(int page, int pageSize)
        {
            var totalCount = await _context.Apartments.CountAsync();
            var items = await _context.Apartments
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, totalCount);
        }

        public async Task<Apartment?> GetApartmentById(Guid apartmentId)
        {
            return await _context.Apartments.FindAsync(apartmentId);
        }

        public async Task<(IEnumerable<Apartment> Items, int TotalCount)> GetAvailableApartments(DateTime startDate, DateTime endDate, int page, int pageSize)
        {
            var query = _context.Apartments
                .Where(a => a.IsAvailable && !_context.Bookings.Any(b => b.ApartmentId == a.Id && b.StartDate < endDate && b.EndDate > startDate));
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, totalCount);
        }
    }
}
