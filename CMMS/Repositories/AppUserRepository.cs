using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly IAppDbContext _context;

        public AppUserRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
