using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Workers
{
    public interface IWorkerRepository
    {
        Task AddAsync(Worker worker);
    }
}
