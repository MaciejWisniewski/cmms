using CMMS.Domain.Maintenance.Workers;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.Infrastructure.Domain.Maintenance.Workers
{
    public class WorkerEntityTypeConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.ToTable("Workers", SchemaNames.Maintenance);

            builder.HasKey(w => w.Id);

            builder.Property(w => w.UserName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(w => w.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(w => w.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(w => w.FullName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(w => w.Role)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
