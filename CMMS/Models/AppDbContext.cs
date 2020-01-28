using CMMS.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>, IAppDbContext
    {
        public DbSet<Division> Divisions { get; set; }
        public DbSet<ExclusionType> ExclusionTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DivisionConfiguration());
            builder.ApplyConfiguration(new ExclusionTypeConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
