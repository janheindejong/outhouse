using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.DataAccess;
using OutHouse.Server.Identity;
using OutHouse.Server.Models;

namespace OutHouse.Server.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MeController(
        ILogger<MeController> logger,
        ApplicationDbContext context)
        : ControllerBase
    {

        private readonly ILogger<MeController> _logger = logger;
        private readonly OuthouseRepository _outhouseRepository = new(context);

        private UserContext UserContext => new(HttpContext);

        [HttpGet("outhouses")]
        public async Task<ActionResult<IEnumerable<OuthouseDto>>> GetHouses()
        {
            return Ok((await _outhouseRepository.GetByUserId(UserContext.Id)).Select(x => x.ToDto()));
        }
    }
}
