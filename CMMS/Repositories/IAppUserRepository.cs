using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.Models;

namespace CMMS.Repositories
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
    }
}