using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Presentation.Identity;

namespace OutHouse.Server.Presentation.Controllers
{
    [Authorize]
    [Route("api/outhouses")]
    public class OuthouseController(
        ILogger<OuthouseController> logger,
        ApplicationDbContext context)
            : ControllerBase
    {
        private readonly ILogger<OuthouseController> _logger = logger;

        private UserContext UserContext => new(HttpContext);

        private OuthouseService Service => new(context, UserContext);

        [HttpPost("")]
        public async Task<ActionResult<OuthouseDto>> CreateNew(CreateNewOuthouseRequest request)
        {
            OuthouseDto outhouse = await Service.CreateNewOuthouseAsync(request);
            return CreatedAtAction(nameof(Get), nameof(OuthouseController), new { id = outhouse.Id }, outhouse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OuthouseDto>> Get(Guid id)
        {
            OuthouseDto result = await Service.GetOuthouseByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OuthouseDto>> Delete(Guid id)
        {
            OuthouseDto result = await Service.RemoveOuthouseAsync(id);
            return Ok(result);
        }
    }
}
