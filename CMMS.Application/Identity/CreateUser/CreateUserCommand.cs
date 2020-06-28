using MediatR;

namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public CreateUserCommand(string fullName, string userName, string email, string phoneNumber, string password)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        public string FullName { get; }

        public string UserName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Password { get; }
    }
}
