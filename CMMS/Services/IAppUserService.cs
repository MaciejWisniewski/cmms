using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.DTOs;

namespace CMMS.Services
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUserDto>> GetAllAsync();
    }
}