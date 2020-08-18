using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Database
{
    public class SeedDatabase
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<MaintenanceContext>();
            if (!context.Database.CanConnect())
                return;

            var isRolesTableExists = await IsTableExists(context, SchemaNames.Dbo, "AspNetRoles");
            if (isRolesTableExists)
                await CreateRoles(serviceProvider, context);

            var isUsersTableExists = await IsTableExists(context, SchemaNames.Dbo, "AspNetUsers");
            if (isUsersTableExists && !context.Users.Any())
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
                FullName = "admin",
                PhoneNumber = "111111111"
            };
            await userManager.CreateAsync(admin, "Haslo1!!");
            await userManager.AddToRoleAsync(admin, UserRole.Admin);

            for (int i = 1; i < 3; i++)
            {
                var leader = new AppUser()
                {
                    Email = $"leader{i}@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = $"leader{i}",
                    FullName = $"leader{i}",
                    PhoneNumber = "111111111"
                };
                await userManager.CreateAsync(leader, $"Haslo1!!");
                await userManager.AddToRoleAsync(leader, UserRole.Leader);
            }

            for (int i = 1; i < 11; i++)
            {
                var user = new AppUser()
                {
                    Email = $"user{i}@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = $"user{i}",
                    FullName = $"user{i}",
                    PhoneNumber = "111111111"
                };
                await userManager.CreateAsync(user, $"Haslo1!!");
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

        private static async Task<bool> IsTableExists(DbContext context, string schema, string tableName)
        {
            bool exists;

            var connection = context.Database.GetDbConnection();
            if (connection.State.Equals(ConnectionState.Closed)) await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @$"
                    SELECT 1 FROM sys.tables AS T
                        INNER JOIN sys.schemas AS S ON T.schema_id = S.schema_id
                    WHERE S.Name = '{schema}' AND T.Name = '{tableName}'";
                exists = await command.ExecuteScalarAsync() != null;
            }

            return exists;
        }
    }
}
