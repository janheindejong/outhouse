using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Members;
using OutHouse.Server.Models;

namespace OutHouse.Server.Service
{
    public interface IDbContext
    {
        DbSet<Outhouse> Outhouses { get; }
        DbSet<Member> Members { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
