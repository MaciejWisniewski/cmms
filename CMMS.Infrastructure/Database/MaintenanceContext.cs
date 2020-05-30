using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using CMMS.Infrastructure.Processing.InternalCommands;
using CMMS.Infrastructure.Processing.Outbox;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Infrastructure.Database
{
    public class MaintenanceContext : IdentityDbContext<AppUser, AppRole, TypedIdValueBase>
    {
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }

        public MaintenanceContext(DbContextOptions<MaintenanceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaintenanceContext).Assembly);
        }
    }
}
