using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;
using OutHouse.Server.Domain.Members;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseMemberService(
        IDbContext dbContext,
        IUserContext userContext,
        Guid outhouseId)
            : ServiceBase(dbContext, userContext)
    {

        protected Guid OuthouseId { get; } = outhouseId;


        public async Task<List<MemberDto>> GetMembersAsync()
        {
            Outhouse outhouse = await GetOuthouseAsync();

            if (!outhouse.HasMember(UserContext.Email))
            {
                throw new ForbiddenException("get members of", "Outhouse", OuthouseId);
            }

            return outhouse.Members
                .Select(x => x.ToDto())
                .ToList();
        }

        public async Task<MemberDto> AddMemberAsync(AddMemberRequest request)
        {
            Outhouse outhouse = await GetOuthouseAsync();

            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("add members to", "Outhouse", OuthouseId);
            }

            Member member = outhouse.AddMember(request.MemberEmail, request.MemberName, request.Role);
            await DbContext.SaveChangesAsync();
            return member.ToDto();
        }

        public async Task<MemberDto> RemoveMemberAsync(Guid memberId)
        {
            Outhouse outhouse = await GetOuthouseAsync();

            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("remove members from", "Outhouse", OuthouseId);
            }

            Member member = outhouse.DeleteMember(memberId);
            await DbContext.SaveChangesAsync();
            return member.ToDto();
        }

        protected async Task<Outhouse> GetOuthouseAsync()
        {
            return await DbContext.Outhouses
                .Where(x => x.Id == OuthouseId)
                .Include(x => x.Members)
                .SingleOrDefaultAsync()
                    ?? throw new NotFoundException("Outhouse", OuthouseId);
        }
    }

    public record struct AddMemberRequest(string MemberEmail, string MemberName, Role Role);
}

