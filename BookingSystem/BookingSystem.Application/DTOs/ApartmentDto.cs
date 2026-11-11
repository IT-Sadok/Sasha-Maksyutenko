
namespace BookingSystem.Application.DTOs
{
    public class ApartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string HostId { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public int NumberOfRooms { get; set; }

    }
}
