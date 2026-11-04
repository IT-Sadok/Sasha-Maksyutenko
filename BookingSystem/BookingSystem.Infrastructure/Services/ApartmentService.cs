
using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<ApartmentDto> CreateApartmentAsync(ApartmentDto apartmentDto, string hostId)
        {
            var apartment = await _apartmentRepository.AddApartment(new Apartment
            {
                Name = apartmentDto.Name,
                Price = apartmentDto.Price,
                NumberOfRooms = apartmentDto.NumberOfRooms,
                IsAvailable = apartmentDto.IsAvailable,
                HostId = hostId
            });
            return new ApartmentDto{
                Id = apartment.Id,
                Name = apartment.Name,
                IsAvailable = apartment.IsAvailable,
                Price = apartment.Price,
                NumberOfRooms = apartment.NumberOfRooms,
                HostId = apartment.HostId
            };
        }

        public async Task<ApartmentDto> GetApartmentByIdAsync(Guid apartmentId)
        {
            var apartment = await _apartmentRepository.GetApartmentById(apartmentId);
            return new ApartmentDto
            {
                Id = apartment.Id,
                Name = apartment.Name,
                IsAvailable = apartment.IsAvailable,
                Price = apartment.Price,
                NumberOfRooms = apartment.NumberOfRooms,
                HostId = apartment.HostId,
            };
        }

        public async Task<IEnumerable<ApartmentDto>> GetAvailableApartmentsAsync(DateTime start, DateTime end)
        {
            var apartments = await _apartmentRepository.GetAvailableApartments(start, end);
            return apartments.Select(apartment => new ApartmentDto
            {
                Id = apartment.Id,
                Name = apartment.Name,
                IsAvailable = apartment.IsAvailable,
                Price = apartment.Price,
                NumberOfRooms = apartment.NumberOfRooms,
                HostId = apartment.HostId,
            });
        }

        public async Task<IEnumerable<ApartmentDto>> ListApartmentsAsync()
        {
            var apartments = await _apartmentRepository.GetAll();
            return apartments.Select(apartment => new ApartmentDto
            {
                Id = apartment.Id,
                Name = apartment.Name,
                IsAvailable = apartment.IsAvailable,
                Price = apartment.Price,
                NumberOfRooms = apartment.NumberOfRooms,
                HostId = apartment.HostId,
            });
        }
    }
}
