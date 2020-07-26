using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Workers.Events
{
    public class WorkerUpdatedDomainEvent : DomainEventBase
    {
        public WorkerId WorkerId { get; }

        public WorkerUpdatedDomainEvent(WorkerId workerId)
        {
            WorkerId = workerId;
        }
    }
}
