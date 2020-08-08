using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.ServiceTypes
{
    public interface IServiceTypeRepository
    {
        Task AddAsync(ServiceType serviceType);
    }
}
