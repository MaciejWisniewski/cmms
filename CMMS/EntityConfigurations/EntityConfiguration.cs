using CMMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.EntityConfigurations
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(e => e.DivisionId)
                .IsRequired();
        }
    }
}
