using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Identity.Events
{
    public class UserUpdatedDomainEvent : DomainEventBase
    {
        public Guid UserId { get; }

        public UserUpdatedDomainEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
