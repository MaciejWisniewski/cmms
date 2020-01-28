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
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();

            await CreateRoles(serviceProvider);

            if (!context.Users.Any())
                await CreateUsers(serviceProvider);

            if(!context.Divisions.Any())
                SeedDivisions(serviceProvider, context);

            context.SaveChanges();
        }

        private static async Task CreateUsers(IServiceProvider serviceProvider)
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

            var superUser = new AppUser()
            {
                Email = "superUser@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "superUser"
            };
            await userManager.CreateAsync(superUser, "superUser@123");
            await userManager.AddToRoleAsync(superUser, UserRole.SuperUser);

            var user = new AppUser()
            {
                Email = "user@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "user"
            };
            await userManager.CreateAsync(user, "User@123");
            await userManager.AddToRoleAsync(user, UserRole.User);
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

        private static void SeedDivisions(IServiceProvider serviceProvider, AppDbContext context)
        {
            var divisions = new List<Division>
            {
                new Division() {Name = "Wydział produkcji celulozy"},
                new Division() {Name = "Maszyna papiernicza nr 1"},
                new Division() {Name = "Maszyna papiernicza nr 2"},
                new Division() {Name = "Maszyna papiernicza nr 3"},
                new Division() {Name = "Korowalnia i rębalnia"}
            };

            context.Divisions.AddRange(divisions);
        }
    }
}
