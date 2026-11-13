

using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
using Mapster;

namespace BookingSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        public BookingService(IBookingRepository bookingRepository,
                              IApartmentRepository apartmentRepository)
        {
            _bookingRepository = bookingRepository;
            _apartmentRepository = apartmentRepository;
        }
        public async Task<BookingDTO> CreateBookingAsync(CreateBookingDTO bookingDto, string userId)
        {
            var apartment = await _apartmentRepository.GetApartmentById(bookingDto.ApartmentId)
                ?? throw new Exception("The apartment was not found.");
            var totalPrice = (bookingDto.EndDate - bookingDto.StartDate).Days * apartment.Price;
            var booking = await _bookingRepository.CreateBooking(new Booking
            {
                UserId = userId,
                ApartmentId = bookingDto.ApartmentId,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                TotalPrice = totalPrice,
            }) ?? throw new Exception("The apartment is not available for the selected dates.");

            var result = booking.Adapt<BookingDTO>();
            result.ApartmentName = apartment.Name;
            return result;
        }

        public async Task<PagedResult<BookingDTO>> GetBookingsByUserIdAsync(string userId, int page, int pageSize)
        {
            var (items, totalCount) = await _bookingRepository.GetBookingsByUserId(userId, page, pageSize);
            var dtos = items.Select(
                booking =>
                {
                    var dto = booking.Adapt<BookingDTO>();
                    dto.ApartmentName = booking.Apartment.Name;
                    return dto;
                });
            return new PagedResult<BookingDTO>(dtos, totalCount, page, pageSize);
        }
    }
}
