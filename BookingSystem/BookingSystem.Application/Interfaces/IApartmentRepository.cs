using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface IApartmentRepository
    {
        Task<Apartment> GetApartmentById(Guid apartmentId);
        Task<IEnumerable<Apartment>> GetAll();
        Task<IEnumerable<Apartment>> GetAvailableApartments(DateTime startDate, DateTime endDate);
        Task<Apartment> AddApartment(Apartment apartment);
    }
}
