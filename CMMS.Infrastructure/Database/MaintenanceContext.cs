using CMMS.Domain.Identity;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.SeedWork;
using CMMS.Infrastructure.Processing.InternalCommands;
using CMMS.Infrastructure.Processing.Outbox;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CMMS.Infrastructure.Database
{
    public class MaintenanceContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }
        public DbSet<Resource> Resources { get; set; }

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
