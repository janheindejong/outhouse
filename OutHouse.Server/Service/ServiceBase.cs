namespace OutHouse.Server.Service
{
    public class ServiceBase(IDbContext dbContext, IUserContext userContext)
    {
        public IDbContext DbContext { get; } = dbContext;
        public IUserContext UserContext { get; } = userContext;
    }
}
