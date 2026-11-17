
namespace BookingSystem.DataMigration.Models
{
    public class UserImportModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public List<ApartmentImportModel> Apartments { get; set; } = new();
    }
}
