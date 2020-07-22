using CMMS.Domain.Maintenance.Resources;
using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.Infrastructure.Domain.Maintenance.Resources
{
    internal class ResourceEntityTypeConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources", SchemaNames.Maintenance);

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(255);

            builder.HasOne<Resource>()
                .WithMany()
                .HasForeignKey(r => r.ParentId);
        }
    }
}
