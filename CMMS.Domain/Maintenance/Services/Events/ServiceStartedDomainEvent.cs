using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Events
{
    public class ServiceStartedDomainEvent : DomainEventBase
    {
        public ServiceId ServiceId { get; }

        public ServiceStartedDomainEvent(ServiceId serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
