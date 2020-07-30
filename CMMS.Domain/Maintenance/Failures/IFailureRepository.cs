using System.Threading.Tasks;

namespace CMMS.Domain.Maintenance.Failures
{
    public interface IFailureRepository
    {
        Task AddAsync(Failure failure);
    }
}