using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Events
{
    public class ResourceCreatedDomainEvent : DomainEventBase
    {
        public ResourceCreatedDomainEvent(ResourceId resourceId)
        {
            ResourceId = resourceId;
        }

        public ResourceId ResourceId { get; }
    }
}
