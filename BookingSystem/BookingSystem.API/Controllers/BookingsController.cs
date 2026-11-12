using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IUserContext _userContext;

        public BookingsController(IBookingService bookingService, IUserContext userContext)
        {
            _bookingService = bookingService;
            _userContext = userContext;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO bookingDTO)
        {
            var result = await _bookingService.CreateBookingAsync(bookingDTO, _userContext.UserId);
            return Ok(result);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetMyBookings()
        {
            var result = await _bookingService.GetBookingsByUserIdAsync(_userContext.UserId);
            return Ok(result);
        }
    }
}
