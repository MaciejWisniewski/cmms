using System;

namespace CMMS.Application.Maintenance.Services.GetServicesByWorkerAccesses
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public Guid ResouceId { get; set; }
        public string ResourceName { get; set; }
        public Guid TypeId { get; set; }
        public string TypeName { get; set; }
        public Guid ScheduledWorkerId { get; set; }
        public string ScheduledWorkerName { get; set; }
        public Guid? ActualWorkerId { get; set; }
        public string ActualWorkerName { get; set; }
        public DateTime ScheduledStartDateTime { get; set; }
        public DateTime ScheduledEndDateTime { get; set; }
        public DateTime? ActualStartDateTime { get; set; }
        public DateTime? ActualEndDateTime { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }
}
