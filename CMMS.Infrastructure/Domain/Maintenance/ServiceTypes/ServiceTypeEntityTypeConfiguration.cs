using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.Infrastructure.Domain.Maintenance.ServiceTypes
{
    internal sealed class ServiceTypeEntityTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.ToTable("ServiceTypes", SchemaNames.Maintenance);

            builder.HasKey(st => st.Id);

            builder.Property(st => st.Name)
                .HasMaxLength(80)
                .IsRequired();

            builder.HasIndex(st => st.Name)
                .IsUnique();
        }
    }
}
