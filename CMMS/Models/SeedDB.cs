using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Models
{
    public class SeedDB
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<IdentityDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            context.Database.EnsureCreated();

            await CreateRoles(serviceProvider);

            if (!context.Users.Any())
            {
                AppUser user = new AppUser()
                {
                    Email = "admin@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin"
                };
                
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, UserRole.Admin);
            }
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roles = new List<AppRole>
            {
                new AppRole() {Name = "Admin"},
                new AppRole() {Name = "SuperUser"},
                new AppRole() {Name = "User"}
            };

            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            foreach (var role in roles)
                if (await roleManager.FindByNameAsync(role.Name) == null)
                    await roleManager.CreateAsync(role);
        }
    }
}
