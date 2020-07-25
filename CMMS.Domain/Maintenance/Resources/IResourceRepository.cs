using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Resources
{
    public interface IResourceRepository
    {
        Task<Resource> GetByIdAsync(ResourceId id);

        Task AddAsync(Resource resource);
    }
}