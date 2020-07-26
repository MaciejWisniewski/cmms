using CMMS.Application.Configuration.Commands;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticateCommand : CommandBase<AuthenticationResult>
    {
        public string UserName { get; }
        public string Password { get; }

        public AuthenticateCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
