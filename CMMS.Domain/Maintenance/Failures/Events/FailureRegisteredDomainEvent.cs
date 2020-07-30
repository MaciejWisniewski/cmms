using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Failures.Events
{
    public class FailureRegisteredDomainEvent : DomainEventBase
    {
        public FailureId FailureId { get; }

        public FailureRegisteredDomainEvent(FailureId failureId)
        {
            FailureId = failureId;
        }
    }
}
