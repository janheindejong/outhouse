using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Presentation.Identity;
using OutHouse.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OutHouse.Server.Presentation.Controllers
{
    [Authorize]
    [Route("api/outhouses")]
    public class OuthouseBookingController(
        ILogger<OuthouseBookingController> logger,
        ApplicationDbContext dbContext)
            : ControllerBase
    {
        private readonly ILogger<OuthouseBookingController> _logger = logger;
        
        private UserContext UserContext => new(HttpContext);

        private OuthouseBookingService OuthouseBookingService => new(dbContext, UserContext);


        [HttpPost("/{outhouseId}/bookings")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<BookingDto>> AddBooking(Guid outhouseId, AddBookingRequest request)
        {
            await OuthouseBookingService.AddBookingAsync(outhouseId, request);
            return Created(); 
        }

        [HttpGet("/{outhouseId}/bookings")]
        public async Task<ActionResult<List<BookingDto>>> Get(Guid outhouseId)
        {
            List<BookingDto> result = await OuthouseBookingService.GetBookingsAsync(outhouseId);
            return Ok(result);
        }

        [HttpPost("/{outhouseId}/members/{bookingId}:approve")]
        public async Task<ActionResult> ApproveBooking(Guid outhouseId, Guid bookingId)
        {
            await OuthouseBookingService.ApproveBookingAsync(outhouseId, bookingId);
            return Ok();
        }

        [HttpPost("/{outhouseId}/members/{bookingId}:reject")]
        public async Task<ActionResult> RejectBooking(Guid outhouseId, Guid bookingId)
        {
            await OuthouseBookingService.RejectBookingAsync(outhouseId, bookingId);
            return Ok();
        }

        [HttpPost("/{outhouseId}/members/{bookingId}:cancel")]
        public async Task<ActionResult> CancelBooking(Guid outhouseId, Guid bookingId)
        {
            await OuthouseBookingService.CancelBookingAsync(outhouseId, bookingId);
            return Ok();
        }
    }
}
