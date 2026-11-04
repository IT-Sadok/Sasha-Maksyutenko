

namespace BookingSystem.Domain.Entities
{
    public class Booking
    {
        public Apartment Apartment { get; set; } = null!;
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public Guid ApartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
