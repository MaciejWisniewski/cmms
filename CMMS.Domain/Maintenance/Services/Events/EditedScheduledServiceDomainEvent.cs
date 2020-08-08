using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Services.Events
{
    public class EditedScheduledServiceDomainEvent : DomainEventBase
    {
        public ServiceId ServiceId { get; }

        public EditedScheduledServiceDomainEvent(ServiceId serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
