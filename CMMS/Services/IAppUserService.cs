using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CMMS.Services
{
    public interface IAppUserService
    {
        Task<AppUserDto> GetByUserNameAsync(string userName);
        Task<IEnumerable<AppUserDto>> GetAllAsync();
        Task<IdentityResult> CreateAsync(AppUserDto userDto);
        Task<IdentityResult> AddToRoleAsync(AppUserDto userDto);
    }
}