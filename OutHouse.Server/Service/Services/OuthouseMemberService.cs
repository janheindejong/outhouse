using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;
using OutHouse.Server.Domain.Members;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseMemberService(
        IDbContext dbContext,
        IUserContext userContext)
            : ServiceBase(dbContext, userContext)
    {

        public async Task<List<MemberDto>> GetMembersAsync(Guid outhouseId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);

            if (!outhouse.HasMember(UserContext.Email))
            {
                throw new ForbiddenException("get members of", "Outhouse", outhouseId);
            }

            return outhouse.Members
                .Select(x => x.ToDto())
                .ToList();
        }

        public async Task<MemberDto> AddMemberAsync(Guid outhouseId, AddMemberRequest request)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);

            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("add members to", "Outhouse", outhouseId);
            }

            Member member = outhouse.AddMember(request.MemberEmail, request.MemberName, request.Role);
            await DbContext.SaveChangesAsync();
            return member.ToDto();
        }

        public async Task<MemberDto> RemoveMemberAsync(Guid outhouseId, Guid memberId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);

            if (!outhouse.HasAdmin(UserContext.Email))
            {
                throw new ForbiddenException("remove members from", "Outhouse", outhouseId);
            }

            Member member = outhouse.DeleteMember(memberId);
            await DbContext.SaveChangesAsync();
            return member.ToDto();
        }

        protected async Task<Outhouse> GetOuthouseAsync(Guid outhouseId)
        {
            return await DbContext.Outhouses
                .Where(x => x.Id == outhouseId)
                .Include(x => x.Members)
                .SingleOrDefaultAsync()
                    ?? throw new NotFoundException("Outhouse", outhouseId);
        }
    }

    public record struct AddMemberRequest(string MemberEmail, string MemberName, Role Role);
}

