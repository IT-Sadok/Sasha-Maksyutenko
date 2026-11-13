using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface IApartmentRepository
    {
        Task<Apartment> GetApartmentById(Guid apartmentId);
        Task<(IEnumerable<Apartment>Items,int TotalCount)> GetAll(int page, int pageSize);
        Task<(IEnumerable<Apartment>Items, int TotalCount)> GetAvailableApartments(DateTime startDate, DateTime endDate, int page, int pageSize);
        Task<Apartment> AddApartment(Apartment apartment);
    }
}
