using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Application.Mappers;
using OutHouse.Server.Application.Services;
using OutHouse.Server.DataAccess;

namespace OutHouse.Server.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/outhouses")]
    public class OuthousesController(
        ILogger<OuthousesController> logger,
        ApplicationDbContext context)
            : ApplicationBaseController
    {
        private readonly ILogger<OuthousesController> _logger = logger;

        private OuthouseService Service => new(context, UserContext);

        [HttpPost("")]
        public async Task<ActionResult<OuthouseDto>> CreateNew(CreateNewOuthouseRequest request)
        {
            return await ExecuteWithExceptionHandling(
                Service.CreateNewOuthouseAsync(request),
                new CreatedAtActionResultFactory<OuthouseDto>(nameof(Get), this));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OuthouseDto>> Get(Guid id)
        {
            return await ExecuteWithExceptionHandling(
                Service.GetOuthouseByIdAsync(id),
                new OkResultFactory<OuthouseDto>());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OuthouseDto>> Delete(Guid id)
        {
            return await ExecuteWithExceptionHandling(
                Service.RemoveOuthouseAsync(id), 
                new OkResultFactory<OuthouseDto>());
        }
    }
}
