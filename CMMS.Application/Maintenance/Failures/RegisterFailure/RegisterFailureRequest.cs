using System;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class RegisterFailureRequest
    {
        public Guid ResourceId { get; set; }
        public string ProblemDescription { get; set; }
    }
}
