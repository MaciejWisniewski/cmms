using CMMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMMS.Infrastructure.Processing.InternalCommands
{
    internal class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommand>
    {
        public void Configure(EntityTypeBuilder<InternalCommand> builder)
        {
            builder.ToTable("InternalCommands", SchemaNames.Application);

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();
        }
    }
}
