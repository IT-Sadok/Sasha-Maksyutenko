
using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Services;
using Moq;
using Shouldly;

namespace BookingSystem.Tests
{
    public class ApartmentServiceTests
    {
        private readonly Mock<IApartmentRepository> _apartmentRepositoryMock;
        private readonly ApartmentService _apartmentService;

        public ApartmentServiceTests()
        {
            _apartmentRepositoryMock = new Mock<IApartmentRepository>();
            _apartmentService = new ApartmentService(_apartmentRepositoryMock.Object);
        }

        [Fact]
        public async Task  GetApartmentbyIdAsync_WhenApartmentExists_ReturnsApartmentDto()
        {
            //Arrange
            var apartmentId = Guid.NewGuid();
            var aparment = new Apartment
            {
                Id = apartmentId,
                Name = "Test Apartment",
                HostId = "host1",
                IsAvailable = true,
                Price = 100,
                NumberOfRooms = 2
            };
            _apartmentRepositoryMock.Setup(x=>x.GetApartmentById(apartmentId)).ReturnsAsync(aparment);
            //Act
            var result=await _apartmentService.GetApartmentByIdAsync(apartmentId);
            //Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Test Apartment");
        }

        [Fact]
        public async Task GetApartmentByIdAsync_WhenApartmentNotFound_ReturnsResult()
        {
            //Arrange
            var apartmentId=Guid.NewGuid();
            _apartmentRepositoryMock.Setup(x => x.GetApartmentById(apartmentId)).ReturnsAsync((Apartment?)null);
            
            //Act
            var result=await _apartmentService.GetApartmentByIdAsync(apartmentId);
            
            //Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task ListApartmentsAsync_ReturnsAllApartments()
        {
            //Arrange
            var apartments = new List<Apartment>
            {
                new Apartment { Id = Guid.NewGuid(), Name = "Apartment 1", HostId = "host1", IsAvailable = true, Price = 100, NumberOfRooms = 2 },
                new Apartment { Id = Guid.NewGuid(), Name = "Apartment 2", HostId = "host2", IsAvailable = false, Price = 150, NumberOfRooms = 3 }
            };
            _apartmentRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(apartments);
            
            //Act
            var result = await _apartmentService.ListApartmentsAsync();
            
            //Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(2);
        }

        [Fact]
        public async Task CreateApartmentAsync_ReturnsApartmentDtoWithCorrectHostId()
        {
            // Arrange
            var hostId = "host1";
            var apartmentDto = new ApartmentDto
            {
                Name = "New Apartment",
                IsAvailable = true,
                Price = 200,
                NumberOfRooms = 3
            };
            _apartmentRepositoryMock.Setup(x=>x.AddApartment(It.IsAny<Apartment>())).ReturnsAsync((Apartment apartment) => { apartment.Id=Guid.NewGuid(); return apartment; });
           
            // Act
            var result=await _apartmentService.CreateApartmentAsync(apartmentDto, hostId);
            
            // Assert
            result.ShouldNotBeNull();
            result.HostId.ShouldBe(hostId);
            result.Name.ShouldBe("New Apartment");
        }
    }
}
