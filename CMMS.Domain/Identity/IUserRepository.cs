using System.Threading.Tasks;

namespace CMMS.Domain.Identity
{
    public interface IUserRepository
    {
        Task<AppUser> GetByUserNameAsync(string userName);
        Task AddAsync(AppUser user, string password);
        Task AddToRoleAsync(AppUser user, string role);
    }
}
