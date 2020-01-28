using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.DTOs;

namespace CMMS.Services
{
    public interface IExclusionTypeService
    {
        Task<IEnumerable<ExclusionTypeDto>> GetAllAsync();
    }
}