using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Resources
{
    public interface IResourceRepository
    {
        Task AddAsync(Resource resource);
    }
}