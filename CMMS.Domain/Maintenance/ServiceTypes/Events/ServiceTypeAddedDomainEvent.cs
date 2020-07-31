using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.ServiceTypes.Events
{
    public class ServiceTypeAddedDomainEvent : DomainEventBase
    {
        public ServiceTypeId ServiceTypeId { get; }

        public ServiceTypeAddedDomainEvent(ServiceTypeId serviceTypeId)
        {
            ServiceTypeId = serviceTypeId;
        }
    }
}
