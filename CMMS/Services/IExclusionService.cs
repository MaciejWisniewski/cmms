using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.DTOs;

namespace CMMS.Services
{
    public interface IExclusionService
    {
        Task<IEnumerable<ExclusionDto>> GetByEntityIdAsync(int entityId);
    }
}