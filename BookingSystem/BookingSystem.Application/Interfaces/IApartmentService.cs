
using BookingSystem.Application.DTOs;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Application.Interfaces
{
    public interface IApartmentService
    {
        Task<ApartmentDto?> GetApartmentByIdAsync(Guid apartmentId);
        Task<IEnumerable<ApartmentDto>> ListApartmentsAsync();
        Task<IEnumerable<ApartmentDto>> GetAvailableApartmentsAsync(DateTime start, DateTime end);
        Task<ApartmentDto> CreateApartmentAsync(ApartmentDto apartmentDto, string hostId);
        Task<ApartmentDto> UpsertApartmentAsync(ApartmentDto apartmentDto, string hostId);
    }
}
