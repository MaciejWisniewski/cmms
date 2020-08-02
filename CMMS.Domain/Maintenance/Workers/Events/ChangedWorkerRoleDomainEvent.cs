using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Workers.Events
{
    public class ChangedWorkerRoleDomainEvent : DomainEventBase
    {
        public WorkerId WorkerId { get; }

        public ChangedWorkerRoleDomainEvent(WorkerId workerId)
        {
            WorkerId = workerId;
        }
    }
}
