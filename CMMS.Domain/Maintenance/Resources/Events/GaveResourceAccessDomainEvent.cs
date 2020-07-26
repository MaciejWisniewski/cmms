using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Events
{
    public class GaveResourceAccessDomainEvent : DomainEventBase
    {
        public ResourceId ResourceId { get; }
        public WorkerId WorkerId { get; }

        public GaveResourceAccessDomainEvent(ResourceId resourceId, WorkerId workerId)
        {
            ResourceId = resourceId;
            WorkerId = workerId;
        }
    }
}
