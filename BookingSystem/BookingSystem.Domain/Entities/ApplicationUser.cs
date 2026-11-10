
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
