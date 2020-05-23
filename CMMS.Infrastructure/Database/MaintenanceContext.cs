using CMMS.Domain.Areas;
using CMMS.Infrastructure.Processing.InternalCommands;
using CMMS.Infrastructure.Processing.Outbox;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMMS.Infrastructure.Database
{
    public class MaintenanceContext : DbContext
    {
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<InternalCommand> InternalCommands { get; set; }

        public MaintenanceContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MaintenanceContext).Assembly);
        }
    }
}
