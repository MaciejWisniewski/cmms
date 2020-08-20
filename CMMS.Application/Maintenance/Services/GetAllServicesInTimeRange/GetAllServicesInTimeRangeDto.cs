using System;

namespace CMMS.Application.Maintenance.Services.GetAllServicesInTimeRange
{
    public class GetAllServicesInTimeRangeDto
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public Guid TypeId { get; set; }
        public string TypeName { get; set; }
        public DateTime ScheduledStartDateTime { get; set; }
        public DateTime ScheduledEndDateTime { get; set; }
        public DateTime? ActualStartDateTime { get; set; }
        public DateTime? ActualEndDateTime { get; set; }
    }
}
