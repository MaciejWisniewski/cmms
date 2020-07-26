using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Identity.Events
{
    public class UserDeactivatedDomainEvent : DomainEventBase
    {
        public Guid UserId { get; }

        public UserDeactivatedDomainEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
