

using BookingSystem.Application.DTOs;

namespace BookingSystem.Application.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<HostRevenueDto>> GetRevenueByHostAsync();
        Task<IEnumerable<HostBookingCountDto>> GetBookingCountByHostAsync();
        Task<IEnumerable<HostRevenueDto>> GetTopHostsByRevenueAsync(decimal threshold);
        Task<PriceQuantilesDto> GetPriceQuantilesAsync();
        Task<IEnumerable<HostAvgDurationDto>> GetAvgBookingDurationByHostAsync();
    }
}
