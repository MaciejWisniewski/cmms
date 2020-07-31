using CMMS.Domain.Maintenance.Failures;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Maintenance.Failures
{
    public class FailureRepository : IFailureRepository
    {
        private readonly MaintenanceContext _context;

        public FailureRepository(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Failure failure)
        {
            await _context.Failures.AddAsync(failure);
        }

        public async Task<Failure> GetByIdAsync(FailureId failureId)
        {
            return await _context.Failures.FirstOrDefaultAsync(o => o.Id == failureId);
        }
    }
}
