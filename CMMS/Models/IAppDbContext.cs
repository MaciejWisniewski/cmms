using Microsoft.EntityFrameworkCore;

namespace CMMS.Models
{
    public interface IAppDbContext
    {
        DbSet<AppUser> Users { get; set; }
        DbSet<Division> Divisions { get; set; }
        DbSet<ExclusionType> ExclusionTypes { get; set; }
        DbSet<Entity> Entities { get; set; }
    }
}
