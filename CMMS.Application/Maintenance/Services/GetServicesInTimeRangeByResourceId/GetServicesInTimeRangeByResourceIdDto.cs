using System;

namespace CMMS.Application.Maintenance.Services.GetServicesInTimeRangeByResourceId
{
    public class GetServicesInTimeRangeByResourceIdDto
    {
        public Guid Id { get; set; }
        public Guid TypeId { get; set; }
        public string TypeName { get; set; }
        public Guid ScheduledWorkerId { get; set; }
        public string ScheduledWorkerUserName { get; set; }
        public Guid ActualWorkerId { get; set; }
        public string ActualWorkerUserName { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime ScheduledStartDateTime { get; set; }
        public DateTime ScheduledEndDateTime { get; set; }
        public DateTime? ActualStartDateTime { get; set; }
        public DateTime? ActualEndDateTime { get; set; }
    }
}
