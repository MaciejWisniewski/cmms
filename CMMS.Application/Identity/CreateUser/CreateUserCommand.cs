using MediatR;
using System;

namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public CreateUserCommand(string fullName, string userName, string email, string phoneNumber, string password, string role)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            Role = role;
        }

        public string FullName { get; }

        public string UserName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Password { get; }

        public string Role { get; }

    }
}
