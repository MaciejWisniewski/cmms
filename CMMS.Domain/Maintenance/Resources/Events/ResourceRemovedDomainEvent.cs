using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Events
{
    public class ResourceRemovedDomainEvent : DomainEventBase
    {
        public ResourceId ResourceId { get; }

        public ResourceRemovedDomainEvent(ResourceId resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
