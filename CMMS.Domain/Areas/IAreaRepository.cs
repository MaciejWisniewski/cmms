using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMMS.Domain.Areas
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAllAsync();
    }
}
