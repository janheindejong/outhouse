using OutHouse.Server.DataAccess;

namespace OutHouse.Server.Handlers
{
    public class BaseService(ApplicationDbContext dbContext, IUserContext userContext)
    {
        public ApplicationDbContext DbContext { get; } = dbContext;
        public IUserContext UserContext { get; } = userContext;
    }

    public interface IUserContext
    {
        public string Email { get; }
    }
}
