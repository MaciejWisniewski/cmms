﻿using Microsoft.AspNetCore.Identity;
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

            if(!context.ExclusionTypes.Any())
                SeedExclusionTypes(serviceProvider, context);

            if(!context.Entities.Any())
                SeedEntities(serviceProvider, context);

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

        private static void SeedDivisions(IServiceProvider serviceProvider, IAppDbContext context)
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

        private static void SeedExclusionTypes(IServiceProvider serviceProvider, IAppDbContext context)
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
        }

        private static void SeedEntities(IServiceProvider serviceProvider, IAppDbContext context)
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
        }
    }
}
