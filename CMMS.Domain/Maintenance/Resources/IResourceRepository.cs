using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Resources
{
    public interface IResourceRepository
    {
        Task<Resource> GetByIdAsync(ResourceId id);

        Task<Resource> GetByIdWithAllDescendantsAndAncestorsAsync(ResourceId id);

        Task AddAsync(Resource resource);

        void Remove(Resource resource);
    }
}