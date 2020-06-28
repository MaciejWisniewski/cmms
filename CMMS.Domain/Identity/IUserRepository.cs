using System.Threading.Tasks;

namespace CMMS.Domain.Identity
{
    public interface IUserRepository
    {
        Task AddAsync(AppUser user, string password);
    }
}
