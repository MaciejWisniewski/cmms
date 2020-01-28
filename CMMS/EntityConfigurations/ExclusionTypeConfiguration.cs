using CMMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.EntityConfigurations
{
    public class ExclusionTypeConfiguration : IEntityTypeConfiguration<ExclusionType>
    {
        public void Configure(EntityTypeBuilder<ExclusionType> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80);
        }
    }
}
