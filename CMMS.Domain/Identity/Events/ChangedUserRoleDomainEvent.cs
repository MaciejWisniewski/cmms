using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Identity.Events
{
    public class ChangedUserRoleDomainEvent : DomainEventBase
    {
        public Guid UserId { get; }
        public string NewRoleName { get; }

        public ChangedUserRoleDomainEvent(Guid userId, string newRoleName)
        {
            UserId = userId;
            NewRoleName = newRoleName;
        }
    }
}
