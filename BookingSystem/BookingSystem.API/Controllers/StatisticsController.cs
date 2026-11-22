using BookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("revenue-by-host")]
        public async Task<IActionResult> GetRevenueByHost()
        {
            var result = await _statisticsService.GetRevenueByHostAsync();
            return Ok(result);
        }

        [HttpGet("booking-count-by-host")]
        public async Task<IActionResult> GetBookingCountByHost()
        {
            var result = await _statisticsService.GetBookingCountByHostAsync();
            return Ok(result);
        }

        [HttpGet("top-hosts")]
        public async Task<IActionResult> GetTopHostsByRevenueAsync([FromQuery] decimal threshold)
        {
            var result = await _statisticsService.GetTopHostsByRevenueAsync(threshold);
            return Ok(result);
        }

        [HttpGet("price-quantiles")]
        public async Task<IActionResult> GetPriceQuantiles()
        {
            var result = await _statisticsService.GetPriceQuantilesAsync();
            return Ok(result);
        }

        [HttpGet("avg-duration-by-host")]
        public async Task<IActionResult> GetAvgBookingDurationByHost()
        {
            var result = await _statisticsService.GetAvgBookingDurationByHostAsync();
            return Ok(result);
        }
    }
}
