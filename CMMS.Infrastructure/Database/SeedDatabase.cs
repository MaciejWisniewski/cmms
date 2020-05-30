using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
