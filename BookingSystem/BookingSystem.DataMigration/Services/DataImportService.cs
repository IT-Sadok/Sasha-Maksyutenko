
using BookingSystem.DataMigration.Models;
using BookingSystem.Domain.Entities;
using BookingSystem.Domain.Enums;
using BookingSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BookingSystem.DataMigration.Services
{
    public class DataImportService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public DataImportService(AppDbContext context, UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task ImportAsync(string filePath)
        {
            const int batchSize = 100;
            var userCount = 0;
            var apartmentCount = 0;
            var skippedUsers = 0;
            var skippedApartments = 0;
            using var stream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            await foreach(var user in JsonSerializer.DeserializeAsyncEnumerable<UserImportModel>(stream,options))
            {
                if (user is null) continue;
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.ExternalId == user.Id);
                if(existingUser!=null)
                {
                    skippedUsers++;
                    foreach(var apt in user.Apartments)
                    {
                        var existingApt = await _context.Apartments.AnyAsync(a => a.ExternalId == apt.Id);
                        if(existingApt)
                        {
                            skippedApartments++;
                            continue;
                        }
                        _context.Apartments.Add(new Apartment
                        {
                            Id = Guid.NewGuid(),
                            ExternalId=apt.Id,
                            Name=apt.Name,
                            Price=apt.Price,
                            NumberOfRooms=apt.NumberOfRooms,
                            HostId=existingUser.Id,
                            IsAvailable=true
                        });
                        apartmentCount++;
                    }
                }
                else
                {
                    var appUser = new ApplicationUser
                    {
                        ExternalId = user.Id,
                        Name = user.Name,
                        UserName = user.Email,
                        Role = Enum.Parse<Role>(user.Role)
                    };
                    var result = await _userManager.CreateAsync(appUser, user.Password);
                    if(!result.Succeeded)
                    {
                        Console.WriteLine($"Failed to create user {user.Email}:{string.Join(", ", result.Errors.Select(e => e.Description))}");
                        continue;
                    }
                    userCount++;
                    foreach(var apt in user.Apartments)
                    {
                        var existingApt = await _context.Apartments.AnyAsync(a => a.ExternalId == apt.Id);
                        if(existingApt)
                        {
                            skippedApartments++;
                            continue;
                        }
                        _context.Apartments.Add(new Apartment
                        {
                            Id = Guid.NewGuid(),
                            ExternalId = apt.Id,
                            Name = apt.Name,
                            Price = apt.Price,
                            NumberOfRooms = apt.NumberOfRooms,
                            HostId = appUser.Id,
                            IsAvailable = true
                        });
                        apartmentCount++;
                    }
                }
                if ((userCount + skippedUsers) % batchSize == 0)
                {
                    await using var transaction = await _context.Database.BeginTransactionAsync();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    Console.WriteLine($"Processed {userCount + skippedUsers} users...");
                }
            }

            // Save remaining
            await using var finalTransaction = await _context.Database.BeginTransactionAsync();
            await _context.SaveChangesAsync();
            await finalTransaction.CommitAsync();

            Console.WriteLine($"Import complete!");
            Console.WriteLine($"Users created: {userCount}, skipped: {skippedUsers}");
            Console.WriteLine($"Apartments created: {apartmentCount}, skipped: {skippedApartments}");
        }
    }
}
