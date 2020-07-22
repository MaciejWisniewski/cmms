using CMMS.Domain.Maintenance.Resources;
using CMMS.Infrastructure.Database;
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

        public async Task AddAsync(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
        }
    }
}
