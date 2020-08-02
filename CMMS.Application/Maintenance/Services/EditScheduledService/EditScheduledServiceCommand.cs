using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Services.EditScheduledService
{
    public class EditScheduledServiceCommand : CommandBase
    {
        public Guid ServiceId { get; }
        public Guid ResourceId { get; }
        public Guid ServiceTypeId { get; }
        public Guid ScheduledWorkerId { get; }
        public string Description { get; }
        public DateTime ScheduledStartDateTime { get; }
        public DateTime ScheduledEndDateTime { get; }

        public EditScheduledServiceCommand(
            Guid serviceId,
            Guid resourceId,
            Guid serviceTypeId,
            Guid scheduledWorkerId,
            string description,
            DateTime scheduledStartDateTime,
            DateTime scheduledEndDateTime)
        {
            ServiceId = serviceId;
            ResourceId = resourceId;
            ServiceTypeId = serviceTypeId;
            ScheduledWorkerId = scheduledWorkerId;
            Description = description;
            ScheduledStartDateTime = scheduledStartDateTime;
            ScheduledEndDateTime = scheduledEndDateTime;
        }
    }
}
