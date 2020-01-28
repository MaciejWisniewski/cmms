using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.Models;

namespace CMMS.Repositories
{
    public interface IExclusionRepository
    {
        Task<IEnumerable<Exclusion>> GetByEntityIdAsync(int entityId);
    }
}