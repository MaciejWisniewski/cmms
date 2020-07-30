using System;

namespace CMMS.Domain.SeedWork
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            this.OccurredOn = DateTime.UtcNow;
        }

        public DateTime OccurredOn { get; }
    }
}
