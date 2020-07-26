using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Identity.Rules
{
    public class CannotSetAnAdminRole : IBusinessRule
    {
        private readonly string _roleName;

        public CannotSetAnAdminRole(string roleName)
        {
            _roleName = roleName;
        }

        public bool IsBroken() => _roleName == UserRole.Admin;

        public string Message => "You mustn't set an Admin role";
    }
}
