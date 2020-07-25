using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Identity.Events
{
    public class UserCreatedDomainEvent : DomainEventBase
    {
        public Guid UserId { get; }

        public UserCreatedDomainEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
