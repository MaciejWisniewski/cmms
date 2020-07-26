using MediatR;
using System;

namespace CMMS.Application.Maintenance.Operators.CreateOperator
{
    public class CreateOperatorCommand : IRequest
    {
        public Guid Id { get; }
        public string UserName { get; }
        public string Email { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        public CreateOperatorCommand(Guid id, string userName, string email, string fullName, string phoneNumber)
        {
            Id = id;
            UserName = userName;
            Email = email;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
    }
}
