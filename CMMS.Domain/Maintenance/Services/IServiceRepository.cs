using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Services
{
    public interface IServiceRepository
    {
        Task<Service> GetByIdAsync(ServiceId id);
        Task AddAsync(Service service);
    }
}
