using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMMS.Domain.Identity
{
    public interface IUserRepository
    {
        Task<AppUser> GetByIdAsync(Guid id);
        Task<AppUser> GetByUserNameAsync(string userName);
        Task AddAsync(AppUser user, string password);
        Task AddToRoleAsync(AppUser user, string role);
        Task RemoveAsync(AppUser user);
        Task<IEnumerable<string>> GetRolesAsync(AppUser user);
        Task RemoveFromRolesAsync(AppUser user, IEnumerable<string> roles);
    }
}
