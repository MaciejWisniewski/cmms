using System;
using System.Collections.Generic;
using System.Text;

namespace CMMS.Application.Maintenance.Failures
{
    public class FailureDto
    {
        public Guid Id { get; set; }
        public Guid ResourceId { get; set; }
        public Guid? WorkerId { get; set; }
        public string State { get; set; }
        public string ProblemDescription { get; set; }
        public string Note { get; set; }
        public DateTime OccuredOn { get; set; }
        public DateTime ResolvedOn { get; set; }
    }
}
