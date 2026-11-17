

namespace BookingSystem.DataMigration.Models
{
    public class ApartmentImportModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int NumberOfRooms { get; set; }
    }
}
