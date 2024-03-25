using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Presentation.Identity;
using OutHouse.Server.Models;

namespace OutHouse.Server.Presentation.Controllers
{
    [Authorize]
    [Route("api/outhouses")]
    public class OuthouseMemberController(
        ILogger<OuthouseMemberController> logger,
        ApplicationDbContext dbContext)
            : ControllerBase
    {
        private readonly ILogger<OuthouseMemberController> _logger = logger;
        
        private UserContext UserContext => new(HttpContext);

        private OuthouseMemberService OuthouseMemberService => new(dbContext, UserContext);


        [HttpPost("/{outhouseId}/members")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddMember(Guid outhouseId, AddMemberRequest request)
        {
            await OuthouseMemberService.AddMemberAsync(outhouseId, request);
            return Created();
        }

        [HttpGet("/{outhouseId}/members")]
        public async Task<ActionResult<List<MemberDto>>> Get(Guid outhouseId)
        {
            List<MemberDto> result = await OuthouseMemberService.GetMembersAsync(outhouseId);
            return Ok(result);
        }

        [HttpDelete("/{outhouseId}/members/{memberId}")]
        public async Task<ActionResult<MemberDto>> Delete(Guid outhouseId, Guid memberId)
        {
            MemberDto result = await OuthouseMemberService.RemoveMemberAsync(outhouseId, memberId);
            return Ok(result);
        }
    }
}
