using System;
using System.Threading.Tasks;

namespace CMMS.Domain.Identity
{
    public interface IRoleRepository
    {
        Task<AppRole> GetByIdAsync(Guid id);
        Task<AppRole> GetByNameAsync(string name);
        Task AddAsync(AppRole role);
    }
}
