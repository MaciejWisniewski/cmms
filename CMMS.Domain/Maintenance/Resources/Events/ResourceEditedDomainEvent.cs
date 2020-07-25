using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Events
{
    public class ResourceEditedDomainEvent : DomainEventBase
    {
        public ResourceId ResourceId { get; }

        public ResourceEditedDomainEvent(ResourceId resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
