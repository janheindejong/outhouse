using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Presentation.Identity;

namespace OutHouse.Server.Presentation.Controllers
{
    [Authorize]
    [Route("api/outhouses/{outhouseId}/members")]
    public class OuthouseMemberController(
        ILogger<OuthouseMemberController> logger,
        ApplicationDbContext context, 
        Guid outhouseId)
            : ControllerBase
    {
        private readonly ILogger<OuthouseMemberController> _logger = logger;
        
        private UserContext UserContext => new(HttpContext);

        private OuthouseMemberService Service => new(context, UserContext, outhouseId);

        [HttpPost("")]
        public async Task<ActionResult<MemberDto>> AddMember(AddMemberRequest request)
        {
            MemberDto result = await Service.AddMemberAsync(request);
            return CreatedAtAction(nameof(Get), nameof(OuthouseMemberController), new { outhouseId = result.Id }, result);
        }

        [HttpGet("")]
        public async Task<ActionResult<List<MemberDto>>> Get()
        {
            List<MemberDto> result = await Service.GetMembersAsync();
            return Ok(result);
        }

        [HttpDelete("{memberId}")]
        public async Task<ActionResult<MemberDto>> Delete(Guid memberId)
        {
            MemberDto result = await Service.RemoveMemberAsync(memberId);
            return Ok(result);
        }
    }
}
