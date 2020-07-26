using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UpdateUserCommand : CommandBase
    {
        public Guid UserId { get; set; }
        public string FullName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string Password { get; }
        public string RoleName { get; }

        public UpdateUserCommand(Guid userId, string fullName, string email, string phoneNumber,
            string password, string roleName)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            RoleName = roleName;
        }
    }
}
