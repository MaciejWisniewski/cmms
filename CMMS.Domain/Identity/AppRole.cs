using Microsoft.AspNetCore.Identity;
using System;

namespace CMMS.Domain.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
