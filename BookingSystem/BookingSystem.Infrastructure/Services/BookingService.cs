

using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;

namespace BookingSystem.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        public BookingService(IBookingRepository bookingRepository, IApartmentRepository apartmentRepository)
        {
            _bookingRepository = bookingRepository;
            _apartmentRepository = apartmentRepository;
        }
        public async Task<BookingDTO> CreateBookingAsync(CreateBookingDTO bookingDto)
        {
            var isAvailable = await _bookingRepository.IsBookingAvailable(
                bookingDto.ApartmentId,
                bookingDto.StartDate,
                bookingDto.EndDate);
            if (!isAvailable)
            {
                throw new Exception("The apartment is not available for the selected dates.");
            }

            var apartment = await _apartmentRepository.GetApartmentById(bookingDto.ApartmentId)
                ?? throw new Exception("The apartment was not found.");
            var totalPrice = (bookingDto.EndDate - bookingDto.StartDate).Days * apartment.Price;
            var booking = await _bookingRepository.CreateBooking(new Booking
            {
                UserId = bookingDto.UserId,
                ApartmentId = bookingDto.ApartmentId,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                TotalPrice = totalPrice
            });

            return new BookingDTO
            {
                Id = booking.Id,
                UserId = booking.UserId,
                ApartmentId = booking.ApartmentId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                TotalPrice = booking.TotalPrice
            };
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(string userId)
        {
            var bookings = await _bookingRepository.GetBookingsByUserId(userId);
            return bookings.Select(booking => new BookingDTO
            {
                Id = booking.Id,
                UserId = booking.UserId,
                ApartmentId = booking.ApartmentId,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                TotalPrice = booking.TotalPrice
            });
        }

        public async Task<bool> IsBookingAvailable(Guid apartmentId, DateTime startDate, DateTime endDate)
        {
            return await _bookingRepository.IsBookingAvailable(apartmentId, startDate, endDate);
        }
    }
}
