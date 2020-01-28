using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMMS.Repositories
{
    public class ExclusionTypeRepository : IExclusionTypeRepository
    {
        private readonly IAppDbContext _context;

        public ExclusionTypeRepository(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExclusionType>> GetAllAsync()
        {
            return await _context.ExclusionTypes.ToListAsync();
        }
    }
}
