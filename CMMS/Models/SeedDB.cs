using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CMMS.Models
{
    public class SeedDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<IdentityDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                AppUser user = new AppUser()
                {
                    Email = "ali@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "Ali"
                };
                userManager.CreateAsync(user, "Ali@123");
            }
        }
    }
}
