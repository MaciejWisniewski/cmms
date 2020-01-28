using CMMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.EntityConfigurations
{
    public class ExclusionConfiguration : IEntityTypeConfiguration<Exclusion>
    {
        public void Configure(EntityTypeBuilder<Exclusion> builder)
        {
            builder.Property(e => e.EntityId)
                .IsRequired();

            builder.Property(e => e.ExclusionTypeId)
                .IsRequired();
        }
    }
}
