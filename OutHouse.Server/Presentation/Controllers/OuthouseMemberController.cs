using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;

namespace OutHouse.Server.Presentation.Controllers
{
    [Authorize]
    [Route("api/outhouses/{outhouseId}/members")]
    public class OuthouseMemberController(
        ILogger<OuthouseMemberController> logger,
        ApplicationDbContext context, 
        Guid outhouseId)
            : ApplicationBaseController
    {
        private readonly ILogger<OuthouseMemberController> _logger = logger;

        private OuthouseMemberService Service => new(context, UserContext, outhouseId);

        [HttpPost("")]
        public async Task<ActionResult<MemberDto>> AddMember(AddMemberRequest request)
        {
            return await ExecuteWithExceptionHandling(
                Service.AddMemberAsync(request),
                new CreatedAtActionResultFactory<MemberDto>(nameof(Get), this));
        }

        [HttpGet("")]
        public async Task<ActionResult<List<MemberDto>>> Get()
        {
            return await ExecuteWithExceptionHandling(
                Service.GetMembersAsync(),
                new OkResultFactory<List<MemberDto>>());
        }

        [HttpDelete("{memberId}")]
        public async Task<ActionResult<MemberDto>> Delete(Guid memberId)
        {
            return await ExecuteWithExceptionHandling(
                Service.RemoveMemberAsync(memberId), 
                new OkResultFactory<MemberDto>());
        }
    }
}
