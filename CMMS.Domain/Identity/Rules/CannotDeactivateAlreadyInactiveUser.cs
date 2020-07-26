using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Identity.Rules
{
    public class CannotDeactivateAlreadyInactiveUser : IBusinessRule
    {
        private readonly AppUser _user;

        public CannotDeactivateAlreadyInactiveUser(AppUser user)
        {
            _user = user;
        }

        public bool IsBroken() => !_user.IsActive;

        public string Message => "User is already inactive";
    }
}
