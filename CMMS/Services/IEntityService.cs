using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.DTOs;

namespace CMMS.Services
{
    public interface IEntityService
    {
        Task<IEnumerable<EntityDto>> GetAllAsync();
        Task<bool> EntityExistsAsync(int id);
    }
}