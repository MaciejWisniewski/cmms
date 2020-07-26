using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Operators
{
    public class Operator : Entity, IAggregateRoot
    {
        public OperatorId Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }

        private Operator()
        {
        }

        public static Operator Create(Guid id, string userName, string email, string fullName, string phoneNumber)
        {
            return new Operator(id, userName, email, fullName, phoneNumber);
        }

        private Operator(Guid id, string userName, string email, string fullName, string phoneNumber)
        {
            Id = new OperatorId(id);
            UserName = userName;
            Email = email;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
    }
}
