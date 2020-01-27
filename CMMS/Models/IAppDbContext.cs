using Microsoft.EntityFrameworkCore;

namespace CMMS.Models
{
    public interface IAppDbContext
    {
        DbSet<AppUser> Users { get; set; }
    }
}
