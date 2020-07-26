using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Workers.Events
{
    public class WorkerCreatedDomainEvent : DomainEventBase
    {
        public WorkerId WorkerId { get; }

        public WorkerCreatedDomainEvent(WorkerId workerId)
        {
            WorkerId = workerId;
        }
    }
}
