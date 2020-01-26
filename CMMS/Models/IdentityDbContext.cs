using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Models
{
    public class IdentityDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }
    }
}
