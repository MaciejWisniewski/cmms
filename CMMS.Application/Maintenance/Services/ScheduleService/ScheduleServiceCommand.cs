using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Services.ScheduleService
{
    public class ScheduleServiceCommand : CommandBase<Guid>
    {
        public Guid ResourceId { get; }
        public Guid ServiceTypeId { get; }
        public Guid ScheduledWorkerId { get; }
        public string Description { get; }
        public DateTime ScheduledStartDateTime { get; }
        public DateTime ScheduledEndDateTime { get; }

        public ScheduleServiceCommand(
            Guid resourceId, 
            Guid serviceTypeId, 
            Guid scheduledWorkerId, 
            string description,
            DateTime scheduledStartDateTime,
            DateTime scheduledEndDateTime)
        {
            ResourceId = resourceId;
            ServiceTypeId = serviceTypeId;
            ScheduledWorkerId = scheduledWorkerId;
            Description = description;
            ScheduledStartDateTime = scheduledStartDateTime;
            ScheduledEndDateTime = scheduledEndDateTime;
        }
    }
}
