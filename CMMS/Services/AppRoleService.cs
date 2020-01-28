using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMS.Models;
using Microsoft.AspNetCore.Identity;

namespace CMMS.Services
{
    public class AppRoleService : IAppRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AppRoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
