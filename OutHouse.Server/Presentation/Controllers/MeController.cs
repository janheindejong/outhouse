using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Application.Mappers;
using OutHouse.Server.Application.Services;
using OutHouse.Server.DataAccess;

namespace OutHouse.Server.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MeController(
        ILogger<MeController> logger,
        ApplicationDbContext dbContext)
        : ApplicationBaseController
    {

        private readonly ILogger<MeController> logger = logger;
        private MeService MeService => new(dbContext, UserContext);

        [HttpGet("outhouses")]
        public async Task<ActionResult<List<OuthouseDto>>> GetHouses()
        {
            return await ExecuteWithExceptionHandling(
                MeService.GetOuthousesAsync(),
                new OkResultFactory<List<OuthouseDto>>());
        }
    }
}
