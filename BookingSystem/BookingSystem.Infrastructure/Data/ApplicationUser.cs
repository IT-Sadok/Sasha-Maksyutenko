
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
