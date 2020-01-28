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

            await CreateRoles(serviceProvider, context);

            if (!context.Users.Any())
                await CreateUsers(serviceProvider, context);

            if(!context.Divisions.Any())
                SeedDivisions(context);

            if(!context.ExclusionTypes.Any())
                SeedExclusionTypes(context);

            if(!context.Entities.Any())
                SeedEntities(context);

            if(!context.Exclusions.Any())
                SeedExclusions(context);
        }

        private static async Task CreateUsers(IServiceProvider serviceProvider, AppDbContext context)
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

            context.SaveChanges();
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider, AppDbContext context)
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

            context.SaveChanges();
        }

        private static void SeedDivisions(AppDbContext context)
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

            context.SaveChanges();
        }

        private static void SeedExclusionTypes(AppDbContext context)
        {
            var exclusionTypes = new List<ExclusionType>()
            {
                new ExclusionType() {Name = "Awaria silnika"},
                new ExclusionType() {Name = "Wymiana filtrów"},
                new ExclusionType() {Name = "Smarowanie"},
                new ExclusionType() {Name = "Remont"},
                new ExclusionType() {Name = "Awaria"}
            };

            context.ExclusionTypes.AddRange(exclusionTypes);

            context.SaveChanges();
        }

        private static void SeedEntities(AppDbContext context)
        {
            var divisions = context.Divisions.ToList();

            var entities = new List<Entity>()
            {
                new Entity() {Name = "Kocioł sodowy", Division = divisions.ElementAt(0)},
                new Entity() {Name = "Giętarka", Division = divisions.ElementAt(1)},
                new Entity() {Name = "Wyżynarka", Division = divisions.ElementAt(2)},
                new Entity() {Name = "Prasa", Division = divisions.ElementAt(3)},
                new Entity() {Name = "Mieszalnik", Division = divisions.ElementAt(4)}
            };

            context.Entities.AddRange(entities);

            context.SaveChanges();
        }

        private static void SeedExclusions(AppDbContext context)
        {
            var entities = context.Entities.ToList();
            var exclusionTypes = context.ExclusionTypes.ToList();

            var exclusions = new List<Exclusion>()
            {
                new Exclusion()
                {
                    Entity = entities.ElementAt(0),
                    ExclusionType = exclusionTypes.ElementAt(0),
                    StartDateTime = DateTime.Now.AddDays(-2),
                    EndDateTime = DateTime.Now.AddHours(-3),
                    Note = "Nocna zmiana zepsuła"
                },
                new Exclusion()
                {
                    Entity = entities.ElementAt(0),
                    ExclusionType = exclusionTypes.ElementAt(0),
                    StartDateTime = DateTime.Now.AddDays(-5),
                    EndDateTime = DateTime.Now.AddHours(-10),
                    Note = "Sprzęgło się spaliło "
                },
                new Exclusion()
                {
                    Entity = entities.ElementAt(1),
                    ExclusionType = exclusionTypes.ElementAt(0),
                    StartDateTime = DateTime.Now.AddDays(-2),
                    EndDateTime = DateTime.Now.AddHours(-7)
                },
                new Exclusion()
                {
                    Entity = entities.ElementAt(0),
                    ExclusionType = exclusionTypes.ElementAt(2),
                    StartDateTime = DateTime.Now.AddDays(-2),
                    EndDateTime = DateTime.Now.AddHours(-3)
                }
            };

            context.Exclusions.AddRange(exclusions);

            context.SaveChanges();
        }
    }
}
