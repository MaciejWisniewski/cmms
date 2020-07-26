using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Operators
{
    public interface IOperatorRepository
    {
        Task AddAsync(Operator worker);
    }
}
