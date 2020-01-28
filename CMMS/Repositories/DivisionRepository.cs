using System.Collections.Generic;
using System.Threading.Tasks;
using CMMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Repositories
{
    public interface IDivisionRepository
    {
        Task<IEnumerable<Division>> GetAllAsync();
    }

    public class DivisionRepository : IDivisionRepository
    {
        private readonly AppDbContext _context;

        public DivisionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Division>> GetAllAsync()
        {
            return await _context.Divisions.ToListAsync();
        }
    }
}
