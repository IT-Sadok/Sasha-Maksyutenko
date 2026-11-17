using BookingSystem.DataMigration.Models;
using BookingSystem.DataMigration.Services;
using BookingSystem.Domain.Entities;
using BookingSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory);
        config.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<AppDbContext>();
        services.AddScoped<DataImportService>();
    })
    .Build();

var scope = host.Services.CreateScope();
var importService = scope.ServiceProvider.GetRequiredService<DataImportService>();

var filePath = args.Length > 0 ? args[0] : "Data/seed-data.json";
await importService.ImportAsync(filePath);