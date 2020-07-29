using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Identity.Rules
{
    public class AdminRoleCannotBeSetRule : IBusinessRule
    {
        private readonly string _roleName;

        public AdminRoleCannotBeSetRule(string roleName)
        {
            _roleName = roleName;
        }

        public bool IsBroken() => _roleName == UserRole.Admin;

        public string Message => "You mustn't set an Admin role";
    }
}
