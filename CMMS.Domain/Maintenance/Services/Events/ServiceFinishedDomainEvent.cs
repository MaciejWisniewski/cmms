using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Events
{
    public class ServiceFinishedDomainEvent : DomainEventBase
    {
        public ServiceId ServiceId { get; }

        public ServiceFinishedDomainEvent(ServiceId serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
