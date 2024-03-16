using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseMemberService(
        IDbContext dbContext,
        IUserContext userContext,
        Guid outhouseId)
            : BaseService(dbContext, userContext)
    {
        private readonly Guid outhouseId = outhouseId;

        public async Task<List<MemberDto>> GetMembersAsync()
        {
            Outhouse outhouse = await GetOuthouse();

            if (!outhouse.IsMember(UserContext.Email))
            {
                throw new ForbiddenException("get members of", "Outhouse", outhouseId);
            }

            return outhouse.Members
                .Select(x => x.ToDto())
                .ToList();
        }

        public async Task<MemberDto> AddMemberAsync(AddMemberRequest request)
        {
            Outhouse outhouse = await GetOuthouse();

            if (!outhouse.HasAdminPrivileges(UserContext.Email))
            {
                throw new ForbiddenException("add members to", "Outhouse", outhouseId);
            }

            Member member = outhouse.AddMember(request.MemberEmail, request.MemberName, request.Role);
            await DbContext.SaveChangesAsync();
            return member.ToDto();
        }

        public async Task<MemberDto> RemoveMemberAsync(Guid memberId)
        {
            Outhouse outhouse = await GetOuthouse(); 

            if (!outhouse.HasAdminPrivileges(UserContext.Email))
            {
                throw new ForbiddenException("remove members from", "Outhouse", outhouseId);
            }

            Member member = outhouse.DeleteMember(memberId);
            await DbContext.SaveChangesAsync();
            return member.ToDto();
        }

        private async Task<Outhouse> GetOuthouse()
        {
            return await DbContext.Outhouses
                .Where(x => x.Id == outhouseId)
                .Include(x => x.Members)
                .FirstOrDefaultAsync()
                    ?? throw new NotFoundException("Outhouse", outhouseId);
        }
    }

    public record struct AddMemberRequest(string MemberEmail, string MemberName, Role Role);
}

