using System;

namespace CMMS.Application.Maintenance.Failures.GetFailuresInTimeRangeByResourceId
{
    public class GetFailuresInTimeRangeByResourceIdDto
    {
        public Guid Id { get; set; }
        public Guid? WorkerId { get; set; }
        public string WorkerUserName { get; set; }
        public string State { get; set; }
        public string ProblemDescription { get; set; }
        public string Note { get; set; }
        public DateTime OccurredOn { get; set; }
        public DateTime? ResolvedOn { get; set; }
    }
}
