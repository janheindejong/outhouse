using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Presentation.Identity;

namespace OutHouse.Server.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MeController(
        ILogger<MeController> logger,
        ApplicationDbContext dbContext)
        : ControllerBase
    {

        private readonly ILogger<MeController> logger = logger;

        private UserContext UserContext => new(HttpContext);

        private MeService MeService => new(dbContext, UserContext);

        [HttpGet("outhouses")]
        public async Task<ActionResult<List<OuthouseDto>>> GetHouses()
        {

            List<OuthouseDto> outhouses = await MeService.GetOuthousesAsync();
            return Ok(outhouses);
        }

        [HttpGet("bookings")]
        public async Task<ActionResult<List<BookingDto>>> GetBookings()
        {

            List<BookingDto> bookings = await MeService.GetBookingsAsync();
            return Ok(bookings);
        }
    }
}
