
using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
using BookingSystem.Application.Services;
using Moq;
using Shouldly;

namespace BookingSystem.Tests
{
    public class BookingServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        private readonly Mock<IApartmentRepository> _apartmentRepositoryMock;
        private readonly IBookingService _bookingService;

        public BookingServiceTests()
        {
            _bookingRepositoryMock = new Mock<IBookingRepository>();
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();
            _bookingService = new BookingService(_bookingRepositoryMock.Object, _apartmentRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateBookingAsync_WhenApartmentIsAvailable_ReturnsBookingDto()
        {
            //Arrange
            var userId= "user1";
            var apartmentId=Guid.NewGuid();
            var apartment = new Apartment
            {
                Id = apartmentId,
                Name = "Test Apartment",
                Price = 100,
                HostId = "host1"
            };
            var dto=new CreateBookingDTO
            {
                ApartmentId = apartmentId,
                StartDate = new DateTime(2026,12,1,0,0,0,DateTimeKind.Utc),
                EndDate = new DateTime(2026,12,5,0,0,0,DateTimeKind.Utc)
            };
            _apartmentRepositoryMock.Setup(x=>x.GetApartmentById(apartmentId)).ReturnsAsync(apartment);
            _bookingRepositoryMock.Setup(x => x.CreateBooking(It.IsAny<Booking>())).ReturnsAsync((Booking booking) => { booking.Id=Guid.NewGuid(); return booking; });
            //Act
            var result = await _bookingService.CreateBookingAsync(dto, userId);
            //Assert
            result.ShouldNotBeNull();
            result.TotalPrice.ShouldBe(400);
            result.ApartmentName.ShouldBe("Test Apartment");
        }

        [Fact]
        public async Task GetBookingsByuserIdAsync_ReturnsUserBookings()
        {
            //Arrange
            var userId = "user1";
            var apartmentId = Guid.NewGuid();
            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ApartmentId = apartmentId,
                    StartDate = new DateTime(2026, 12, 1,0,0,0,DateTimeKind.Utc),
                    EndDate = new DateTime(2026, 12, 5,0,0,0,DateTimeKind.Utc),
                    TotalPrice = 400,
                    Apartment=new Apartment{Name="Test Apartment"}
                },
            };
            _bookingRepositoryMock.Setup(x => x.GetBookingsByUserId(userId, 1, 10)).ReturnsAsync((bookings.AsEnumerable(), 1));
            //Act
            var result = await _bookingService.GetBookingsByUserIdAsync(userId,1,10);
            //Assert
            result.ShouldNotBeNull();
            result.Items.Count().ShouldBe(1);
            result.Items.First().ApartmentName.ShouldBe("Test Apartment");
        }
    }
}
