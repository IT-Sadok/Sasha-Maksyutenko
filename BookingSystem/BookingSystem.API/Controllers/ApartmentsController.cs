using BookingSystem.Application.DTOs;
using BookingSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IUserContext _userContext;
        public ApartmentsController(IApartmentService apartmentService, IUserContext userContext)
        {
            _apartmentService = apartmentService;
            _userContext = userContext;
        }

        [HttpPost]
        [Authorize(Roles = "Host")]
        public async Task<IActionResult> CreateApartment([FromBody] CreateApartmentRequest createApartmentRequest)
        {
            var hostId = _userContext.UserId;
            var result = await _apartmentService.CreateApartmentAsync(createApartmentRequest, hostId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetApartments()
        {
            var apartments = await _apartmentService.ListApartmentsAsync();
            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApartment(Guid id)
        {
            var apartment = await _apartmentService.GetApartmentByIdAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            return Ok(apartment);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableApartments([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var apartments = await _apartmentService.GetAvailableApartmentsAsync(start, end);
            return Ok(apartments);
        }
    }
}
