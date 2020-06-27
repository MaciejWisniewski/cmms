using MediatR;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticateCommand : IRequest<JwtTokenDto>
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
