using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        }

        public async Task<AppUser> GetByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task AddAsync(AppUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task AddToRoleAsync(AppUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task RemoveAsync(AppUser user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task RemoveFromRolesAsync(AppUser user, IEnumerable<string> roles)
        {
            await _userManager.RemoveFromRolesAsync(user, roles);
        }


    }
}
