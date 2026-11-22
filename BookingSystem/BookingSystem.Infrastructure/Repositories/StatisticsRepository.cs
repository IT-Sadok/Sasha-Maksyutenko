using System.Reflection;
using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Options;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace BookingSystem.Infrastructure.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly string _connectionString;

        public StatisticsRepository(IOptions<DatabaseOptions> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        private static string ReadSql(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"BookingSystem.Infrastructure.SqlScripts.{fileName}";
            using var stream = assembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public async Task<IEnumerable<HostRevenueDto>> GetRevenueByHostAsync()
        {
            var sql = ReadSql("RevenueByHost.sql");
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<HostRevenueDto>(sql);
        }

        public async Task<IEnumerable<HostBookingCountDto>> GetBookingCountByHostAsync()
        {
            var sql = ReadSql("BookingCountByHost.sql");
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<HostBookingCountDto>(sql);
        }

        public async Task<IEnumerable<HostRevenueDto>> GetTopHostsByRevenueAsync(decimal threshold)
        {
            var sql = ReadSql("TopHostsByRevenue.sql");
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<HostRevenueDto>(sql, new { Threshold = threshold });
        }

        public async Task<PriceQuantilesDto> GetPriceQuantilesAsync()
        {
            var sql = ReadSql("PriceQuantiles.sql");
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstAsync<PriceQuantilesDto>(sql);
        }

        public async Task<IEnumerable<HostAvgDurationDto>> GetAvgBookingDurationByHostAsync()
        {
            var sql = ReadSql("AvgBookingDurationByHost.sql");
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryAsync<HostAvgDurationDto>(sql);
        }
    }
}
