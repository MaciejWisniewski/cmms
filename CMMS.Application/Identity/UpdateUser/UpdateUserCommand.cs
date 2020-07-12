using MediatR;
using System;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public UpdateUserCommand(Guid id, string fullName, string email, string phoneNumber)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Guid Id { get; set; }

        public string FullName { get; }

        public string Email { get; }

        public string PhoneNumber { get; }
    }
}
