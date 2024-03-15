using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Application.Mappers;
using OutHouse.Server.DataAccess;
using OutHouse.Server.Handlers;

namespace OutHouse.Server.Application.Services
{
    public class MeService(
        ApplicationDbContext dbContext,
        IUserContext userContext)
            : BaseService(dbContext, userContext)
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
