using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Infrastructure.Database;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Maintenance.ServiceTypes
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly MaintenanceContext _context;

        public ServiceTypeRepository(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ServiceType serviceType)
        {
            await _context.ServiceTypes.AddAsync(serviceType);
        }
    }
}
