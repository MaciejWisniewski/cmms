using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Identity.Rules
{
    public class UserMustHaveUniqueUsernameAndEmailRule : IBusinessRule
    {
        private readonly IUserUniquenessChecker _userUniquenessChecker;
        private readonly string _userName;
        private readonly string _email;

        public UserMustHaveUniqueUsernameAndEmailRule(
            IUserUniquenessChecker userUniquenessChecker,
            string userName,
            string email)
        {
            _userUniquenessChecker = userUniquenessChecker;
            _userName = userName;
            _email = email;
        }

        public bool IsBroken() => !_userUniquenessChecker.IsUnique(_userName, _email);

        public string Message => "User with the given username or email already exists";
    }
}
