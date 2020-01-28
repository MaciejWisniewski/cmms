using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Repositories
{
    public class EntityRepository : IEntityRepository
    {
        private readonly IAppDbContext _context;

        public EntityRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Entity> GetByIdAsync(int id)
        {
            return await _context.Entities.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _context.Entities.ToListAsync();
        }
    }
}
