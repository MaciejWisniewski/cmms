using CMMS.Domain.SeedWork;
using Microsoft.AspNetCore.Identity;
using System;

namespace CMMS.Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public AppUser()
        {
            Id = Guid.NewGuid();
        }
    }
}
