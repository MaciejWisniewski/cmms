using System;

namespace CMMS.Application.Maintenance.Failures.GetAllFailuresInTimeRange
{
    public class GetAllFailuresInTimeRangeDto
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string State { get; set; }
        public DateTime OccurredOn { get; set; }
        public DateTime? ResolvedOn { get; set; }
    }
}
