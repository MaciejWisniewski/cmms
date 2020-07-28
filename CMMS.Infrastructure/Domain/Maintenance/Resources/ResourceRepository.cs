using CMMS.Domain.Maintenance.Resources;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Maintenance.Resources
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly MaintenanceContext _context;

        public ResourceRepository(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task<Resource> GetByIdAsync(ResourceId id)
        {
            return await _context.Resources.Include(r => r.Children).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Resource> GetByIdWithAllDescendantsAndAncestorsAsync(ResourceId id)
        {
            //TODO: Optimize when performance problem start occuring
            var allResources = await _context.Resources.ToListAsync();
            return allResources.FirstOrDefault(r => r.Id == id);
        }

        public async Task AddAsync(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
        }

        public void Remove(Resource resource)
        {
            _context.Resources.Remove(resource);
        }
    }
}
