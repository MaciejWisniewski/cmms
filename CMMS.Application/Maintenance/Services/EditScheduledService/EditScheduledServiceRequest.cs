using System;

namespace CMMS.Application.Maintenance.Services.EditScheduledService
{
    public class EditScheduledServiceRequest
    {
        public Guid ResourceId { get; set; }
        public Guid ServiceTypeId { get; set; }
        public Guid ScheduledWorkerId { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledStartDateTime { get; set; }
        public DateTime ScheduledEndDateTime { get; set; }
    }
}
