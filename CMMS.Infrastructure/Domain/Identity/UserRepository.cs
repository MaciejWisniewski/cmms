﻿using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
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

        public async Task AddAsync(AppUser user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }

        public async Task AddToRoleAsync(AppUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
