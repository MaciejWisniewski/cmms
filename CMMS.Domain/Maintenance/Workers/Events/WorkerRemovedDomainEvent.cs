using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Workers.Events
{
    public class WorkerRemovedDomainEvent : DomainEventBase
    {
        public WorkerId WorkerId { get; }

        public WorkerRemovedDomainEvent(WorkerId workerId)
        {
            WorkerId = workerId;
        }
    }
}
