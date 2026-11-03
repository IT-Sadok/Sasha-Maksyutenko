
namespace BookingSystem.Application.DTOs
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
