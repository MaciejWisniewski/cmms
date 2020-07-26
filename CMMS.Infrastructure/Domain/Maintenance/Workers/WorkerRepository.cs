using CMMS.Domain.Maintenance.Workers;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Maintenance.Workers
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly MaintenanceContext _context;

        public WorkerRepository(MaintenanceContext context)
        {
            _context = context;
        }

        public async Task<Worker> GetByIdAsync(WorkerId id)
        {
            return await _context.Workers.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddAsync(Worker worker)
        {
            await _context.Workers.AddAsync(worker);
        }
    }
}
