using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Identity.Rules
{
    public class CannotDeactivateAlreadyInactiveUserRule : IBusinessRule
    {
        private readonly AppUser _user;

        public CannotDeactivateAlreadyInactiveUserRule(AppUser user)
        {
            _user = user;
        }

        public bool IsBroken() => !_user.IsActive;

        public string Message => "User is already inactive";
    }
}
