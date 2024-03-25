using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Service.Mappers;

namespace OutHouse.Server.Service.Services
{
    public class MeService(
        IDbContext dbContext,
        IUserContext userContext)
            : ServiceBase(dbContext, userContext)
    {
        public async Task<List<OuthouseDto>> GetOuthousesAsync()
        {
            return await DbContext
                .Members
                .Where(x => x.Email == UserContext.Email)
                .Select(x => x.Outhouse.ToDto())
                .ToListAsync();
        }
    }
}
