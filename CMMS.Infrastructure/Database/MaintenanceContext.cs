using CMMS.Domain.Identity;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Infrastructure.Processing.InternalCommands;
using CMMS.Infrastructure.Processing.Outbox;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Domain.Maintenance.Services;

namespace CMMS.Infrastructure.Database
{
    public class MaintenanceContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Failure> Failures { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

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
