
using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;

using Mapster;

namespace BookingSystem.Application.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<ApartmentDto> CreateApartmentAsync(CreateApartmentRequest createApartmentRequest, string hostId)
        {
            var apartment = await _apartmentRepository.AddApartment(new Apartment
            {
                Name = createApartmentRequest.Name,
                Price = createApartmentRequest.Price,
                NumberOfRooms = createApartmentRequest.NumberOfRooms,
                IsAvailable = true,
                HostId = hostId
            });
            return apartment.Adapt<ApartmentDto>();
        }

        public async Task<ApartmentDto?> GetApartmentByIdAsync(Guid apartmentId)
        {
            var apartment = await _apartmentRepository.GetApartmentById(apartmentId);
            if (apartment == null) return null;
            return apartment.Adapt<ApartmentDto>();
        }

        public async Task<PagedResult<ApartmentDto>> GetAvailableApartmentsAsync(DateTime start, DateTime end,int page, int pageSize)
        {
            var (items, totalCount) = await _apartmentRepository.GetAvailableApartments(start, end, page, pageSize);

            return new PagedResult<ApartmentDto>(items.Adapt<IEnumerable<ApartmentDto>>(), totalCount, page, pageSize);
        }

        public async Task<PagedResult<ApartmentDto>> ListApartmentsAsync(int page, int pageSize)
        {
            var (items, totalCount) = await _apartmentRepository.GetAll(page, pageSize);
            return new PagedResult<ApartmentDto>(items.Adapt<IEnumerable<ApartmentDto>>(), totalCount, page, pageSize);
        }
    }
}
