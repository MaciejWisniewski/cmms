using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Events
{
    public class ServiceScheduledDomainEvent : DomainEventBase
    {
        public ServiceId ServiceId { get; }

        public ServiceScheduledDomainEvent(ServiceId serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
