using MediatR;
using System;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public UpdateUserCommand(Guid id, string fullName, string email, string phoneNumber,
        string password, string roleName)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
            RoleName = roleName;
        }

        public Guid Id { get; set; }

        public string FullName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }

        public string Password { get; }

        public string RoleName { get; }
    }
}
