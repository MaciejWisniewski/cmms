using CMMS.Domain.SeedWork;
using Microsoft.AspNetCore.Identity;

namespace CMMS.Domain.Identity
{
    public class AppUser : IdentityUser<TypedIdValueBase>
    {
        public string FullName { get; set; }
    }
}
