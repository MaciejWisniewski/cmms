using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.Models;

namespace CMMS.Repositories
{
    public interface IEntityRepository
    {
        Task<Entity> GetByIdAsync(int id);
        Task<IEnumerable<Entity>> GetAllAsync();
    }
}