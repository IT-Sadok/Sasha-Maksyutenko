using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;

namespace BookingSystem.Infrastructure.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _repository;

        public StatisticsService(IStatisticsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HostRevenueDto>> GetRevenueByHostAsync()
        {
            return await _repository.GetRevenueByHostAsync();
        }

        public async Task<IEnumerable<HostBookingCountDto>> GetBookingCountByHostAsync()
        {
            return await _repository.GetBookingCountByHostAsync();
        }

        public async Task<IEnumerable<HostRevenueDto>> GetTopHostsByRevenueAsync(decimal threshold)
        {
            return await _repository.GetTopHostsByRevenueAsync(threshold);
        }

        public async Task<PriceQuantilesDto> GetPriceQuantilesAsync()
        {
            return await _repository.GetPriceQuantilesAsync();
        }

        public async Task<IEnumerable<HostAvgDurationDto>> GetAvgBookingDurationByHostAsync()
        {
            return await _repository.GetAvgBookingDurationByHostAsync();
        }
    }
}
