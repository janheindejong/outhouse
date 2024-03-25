namespace OutHouse.Server.Service.Services
{
    public class ServiceBase(IDbContext dbContext, IUserContext userContext)
    {
        public IDbContext DbContext { get; } = dbContext;
        public IUserContext UserContext { get; } = userContext;
    }
}
