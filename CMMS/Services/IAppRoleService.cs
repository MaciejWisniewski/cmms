using System.Threading.Tasks;

namespace CMMS.Services
{
    public interface IAppRoleService
    {
        Task<bool> RoleExistsAsync(string roleName);
    }
}