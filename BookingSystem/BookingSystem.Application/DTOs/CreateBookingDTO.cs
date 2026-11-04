
namespace BookingSystem.Application.DTOs
{
    public class CreateBookingDTO
    {
        public Guid ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
