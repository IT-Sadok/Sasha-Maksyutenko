
using BookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.Interfaces
{
    public interface IUserManagerWrapper
    {
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
    }
}