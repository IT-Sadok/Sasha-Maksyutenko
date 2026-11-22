
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

        public async Task<IEnumerable<Apartment>> GetAll()
        {
            return await _context.Apartments.ToListAsync();
        }

        public async Task<Apartment?> GetApartmentById(Guid apartmentId)
        {
            return await _context.Apartments.FindAsync(apartmentId);
        }

        public async Task<IEnumerable<Apartment>> GetAvailableApartments(DateTime startDate, DateTime endDate)
        {
            return await _context.Apartments
                .Where(a => !_context.Bookings.Any(b => b.ApartmentId == a.Id && b.StartDate < endDate && b.EndDate > startDate))
                .ToListAsync();
        }

        public async Task<Apartment> UpsertApartmentAsync(Apartment apartment)
        {
            var existing = await _context.Apartments.FindAsync(apartment.Id);
            if(existing==null)
            {
                _context.Apartments.Add(apartment);
            }
            else
            {
                existing.Name = apartment.Name;
                existing.Price = apartment.Price;
                existing.NumberOfRooms = apartment.NumberOfRooms;
                existing.IsAvailable = apartment.IsAvailable;
                existing.Metadata = apartment.Metadata;
            }
            await _context.SaveChangesAsync();
            return apartment;
        }
    }
}
