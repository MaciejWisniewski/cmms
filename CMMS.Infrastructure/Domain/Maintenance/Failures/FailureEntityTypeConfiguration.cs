using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.Infrastructure.Domain.Maintenance.Failures
{
    public class FailureEntityTypeConfiguration : IEntityTypeConfiguration<Failure>
    {
        public void Configure(EntityTypeBuilder<Failure> builder)
        {
            builder.ToTable("Failures", SchemaNames.Maintenance);

            builder.HasKey(f => f.Id);

            builder.Property(f => f.ProblemDescription)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne<Resource>()
                .WithMany()
                .HasForeignKey(f => f.ResourceId)
                .IsRequired();

            builder.HasOne<Worker>()
                .WithMany()
                .HasForeignKey(f => f.WorkerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.OwnsOne(f => f.State, f =>
            {
                f.Property(s => s.Value)
                    .HasMaxLength(50)
                    .IsRequired()
                    .HasColumnName(nameof(Failure.State));
            });
        }
    }
}
