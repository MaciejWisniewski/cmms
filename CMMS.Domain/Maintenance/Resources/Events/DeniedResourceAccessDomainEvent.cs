using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Events
{
    public class DeniedResourceAccessDomainEvent : DomainEventBase
    {
        public ResourceId ResourceId { get; }
        public WorkerId WorkerId { get; }

        public DeniedResourceAccessDomainEvent(ResourceId resourceId, WorkerId workerId)
        {
            ResourceId = resourceId;
            WorkerId = workerId;
        }
    }
}
