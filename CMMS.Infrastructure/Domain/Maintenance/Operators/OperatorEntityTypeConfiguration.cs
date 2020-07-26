using CMMS.Domain.Maintenance.Operators;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.Infrastructure.Domain.Maintenance.Operators
{
    public class OperatorEntityTypeConfiguration : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.ToTable("Operators", SchemaNames.Maintenance);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserName)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(o => o.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(o => o.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.FullName)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
