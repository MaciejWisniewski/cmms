using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Workers
{
    public interface IWorkerRepository
    {
        Task<Worker> GetByIdAsync(WorkerId id);
        Task AddAsync(Worker worker);
        void Remove(Worker worker);
    }
}
