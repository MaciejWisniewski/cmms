using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Identity.Events
{
    public class UserUpdatedDomainEvent : DomainEventBase
    {
        public Guid UserId { get; }
        public string FullName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }


        public UserUpdatedDomainEvent(Guid userId, string fullName, string email, string phoneNumber)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
