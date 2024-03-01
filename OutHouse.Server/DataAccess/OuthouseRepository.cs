using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Models;

namespace OutHouse.Server.DataAccess
{
    public class OuthouseRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Outhouse?> GetById(Guid id)
        {
            Outhouse? outhouse = await _context.Outhouses
                .Where(x => x.Id == id)
                .Include(outhouse => outhouse.Members)
                .FirstOrDefaultAsync();
            return outhouse;
        }

        public async Task<IEnumerable<Outhouse>> GetByUserId(Guid userId)
        {
            return await _context.Memberships
                .Where(x => x.UserId == userId)
                .Select(x => x.Outhouse)
                .ToListAsync();
        }

        public void Add(Outhouse outhouse)
        {
            _context.Outhouses.Add(outhouse);
        }
    }

    public interface IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); 
    }
}
