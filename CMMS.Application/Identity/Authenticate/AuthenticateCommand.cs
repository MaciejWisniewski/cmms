using MediatR;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticateCommand : IRequest<AuthenticationResult>
    {
        public AuthenticateCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; }

        public string Password { get; }
    }
}
