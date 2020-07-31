using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services;
using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.Infrastructure.Domain.Maintenance.Services
{
    internal sealed class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services", SchemaNames.Maintenance);

            builder.HasKey(s => s.Id);

            builder.HasOne<Resource>()
                .WithMany()
                .HasForeignKey(s => s.ResourceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ServiceType>()
                .WithMany()
                .HasForeignKey(s => s.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Worker>()
                .WithMany()
                .HasForeignKey(s => s.ScheduledWorkerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Worker>()
                 .WithMany()
                 .HasForeignKey(s => s.ActualWorkerId);
        }
    }
}
