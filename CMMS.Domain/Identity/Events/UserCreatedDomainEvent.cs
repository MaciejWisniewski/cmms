using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Identity.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public Guid UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }
        public string RoleName { get; }

        public UserCreatedDomainEvent(Guid userId, string userName, string email, string fullName, string phoneNumber, string roleName)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            RoleName = roleName;
        }
    }
}
