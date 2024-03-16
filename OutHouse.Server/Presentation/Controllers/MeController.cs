using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;

namespace OutHouse.Server.Presentation.Controllers
{
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
