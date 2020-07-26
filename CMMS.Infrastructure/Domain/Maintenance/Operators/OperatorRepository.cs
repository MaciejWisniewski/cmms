using CMMS.Domain.Maintenance.Operators;
using CMMS.Infrastructure.Database;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Maintenance.Operators
{
    public class OperatorRepository : IOperatorRepository
    {
        private readonly MaintenanceContext _context;

        public OperatorRepository(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Operator worker)
        {
            await _context.Operators.AddAsync(worker);
        }
    }
}
