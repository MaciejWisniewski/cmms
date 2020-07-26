using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserCommand : CommandBase<Guid>
    {
        public string FullName { get; }

        public string UserName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Password { get; }

        public string RoleName { get; }

        public CreateUserCommand(string fullName, string userName, string email, string phoneNumber, string password, string roleName)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            RoleName = roleName;
        }
    }
}
