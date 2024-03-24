using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Models;

namespace OutHouse.Server.Service.Services
{
    public class OuthouseServiceBase(
        IDbContext dbContext,
        IUserContext userContext,
        Guid outhouseId)
            : ServiceBase(dbContext, userContext)
    {
        protected Guid OuthouseId { get; } = outhouseId;

        protected async Task<Outhouse> GetOuthouseAsync()
        {
            return await DbContext.Outhouses
                .Where(x => x.Id == OuthouseId)
                .Include(x => x.Members)
                .SingleOrDefaultAsync()
                    ?? throw new NotFoundException("Outhouse", OuthouseId);
        }
    }
}
