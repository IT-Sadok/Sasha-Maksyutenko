
using BookingSystem.Application.DTOs;

namespace BookingSystem.Application.Interfaces
{
    public interface IApartmentService
    {
        Task<ApartmentDto?> GetApartmentByIdAsync(Guid apartmentId);
        Task<PagedResult<ApartmentDto>> ListApartmentsAsync(int page, int pageSize);
        Task<PagedResult<ApartmentDto>> GetAvailableApartmentsAsync(DateTime start, DateTime end, int page, int pageSize);
        Task<ApartmentDto> CreateApartmentAsync(CreateApartmentRequest request, string hostId);
    }
}
