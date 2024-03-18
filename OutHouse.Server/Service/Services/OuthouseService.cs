using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseService(
            IDbContext dbContext,
            IUserContext userContext)
                : BaseService(dbContext, userContext)
    {

        public async Task<OuthouseDto> GetOuthouseByIdAsync(Guid outhouseId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);
            if (!outhouse.IsMember(UserContext.Email))
            {
                throw new ForbiddenException("read", "Outhouse", outhouseId);
            }

            return outhouse.ToDto();
        }

        public async Task<OuthouseDto> CreateNewOuthouseAsync(CreateNewOuthouseRequest request)
        {
            var newOuthouse = Outhouse.CreateNew(request.Name, UserContext.Email, request.OwnerName);
            DbContext.Outhouses.Add(newOuthouse);
            await DbContext.SaveChangesAsync();
            return newOuthouse.ToDto();
        }

        public async Task<OuthouseDto> RemoveOuthouseAsync(Guid outhouseId)
        {
            Outhouse outhouse = await GetOuthouseAsync(outhouseId);
            if (!outhouse.HasOwnerPrivileges(UserContext.Email))
            {
                throw new ForbiddenException("delete", "Outhouse", outhouseId);
            }

            DbContext.Outhouses.Remove(outhouse);
            await DbContext.SaveChangesAsync();
            return outhouse.ToDto();
        }

        private async Task<Outhouse> GetOuthouseAsync(Guid outhouseId)
        {
            return await DbContext.Outhouses
                .Where(x => x.Id == outhouseId)
                .Include(x => x.Members)
                .SingleOrDefaultAsync()
                    ?? throw new NotFoundException("Outhouse", outhouseId);
        }
    }

    public record struct CreateNewOuthouseRequest(string Name, string OwnerName);
}
