using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Database
{
    public class SeedDatabase
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<MaintenanceContext>();
            context.Database.EnsureCreated();

            await CreateRoles(serviceProvider, context);

            if(! context.Users.Any())
                await CreateUsers(serviceProvider, context);
        }

        private static async Task CreateUsers(IServiceProvider serviceProvider, DbContext context)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            var admin = new AppUser()
            {
                Email = "admin@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin",
            };
            await userManager.CreateAsync(admin, "Admin@123");
            await userManager.AddToRoleAsync(admin, UserRole.Admin);

            for (int i = 1; i < 3; i++)
            {
                var leader = new AppUser()
                {
                    Email = $"leader{i}@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = $"leader{i}"
                };
                await userManager.CreateAsync(leader, $"Leader{i}@123");
                await userManager.AddToRoleAsync(leader, UserRole.Leader);
            }

            for (int i = 1; i < 11; i++)
            {
                var user = new AppUser()
                {
                    Email = $"user{i}@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = $"user{i}"
                };
                await userManager.CreateAsync(user, $"User{i}@123");
                await userManager.AddToRoleAsync(user, UserRole.User);
            }

            context.SaveChanges();
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider, DbContext context)
        {
            var roles = new List<AppRole>
            {
                new AppRole("Admin"),
                new AppRole("Leader"),
                new AppRole("User")
            };

            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            foreach (var role in roles)
                if (await roleManager.FindByNameAsync(role.Name) == null)
                    await roleManager.CreateAsync(role);

            context.SaveChanges();
        }
    }
}
