using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Domain.Identity
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleRepository(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<AppRole> GetByIdAsync(Guid id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<AppRole> GetByNameAsync(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task AddAsync(AppRole role)
        {
            await _roleManager.CreateAsync(role);
        }
    }
}
