using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Repositories
{
    public class ExclusionRepository : IExclusionRepository
    {
        private readonly IAppDbContext _context;

        public ExclusionRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exclusion>> GetByEntityIdAsync(int entityId)
        {
            return await _context.Exclusions.Where(e => e.EntityId == entityId).ToListAsync();
        }
    }
}
