using MediatR;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticateCommand : IRequest<JwtTokenDto>
    {
        public AuthenticateCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }

        public string Password { get; }
    }
}
