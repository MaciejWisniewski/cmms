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

            builder.HasOne(r => r.Parent)
                .WithMany(r => r.Children)
                .HasForeignKey(r => r.ParentId);

            builder.OwnsMany(r => r.Accesses, a =>
            {
                a.WithOwner().HasForeignKey(a => a.ResourceId);
                a.ToTable("ResourceAccesses", SchemaNames.Maintenance);
                a.HasKey(a => new { a.ResourceId, a.WorkerId });
                a.HasOne(a => a.Worker).WithMany().HasForeignKey(a => a.WorkerId);
            });
        }
    }
}
