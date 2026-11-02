

using BookingSystem.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Infrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
