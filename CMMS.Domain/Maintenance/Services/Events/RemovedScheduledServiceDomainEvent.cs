using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Events
{
    public class RemovedScheduledServiceDomainEvent : DomainEventBase
    {
        public ServiceId ServiceId { get; }

        public RemovedScheduledServiceDomainEvent(ServiceId serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
