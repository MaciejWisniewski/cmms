using CMMS.Domain.Maintenance.Services;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Maintenance.Services
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly MaintenanceContext _context;

        public ServiceRepository(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task<Service> GetByIdAsync(ServiceId id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
        }
    }
}
