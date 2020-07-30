using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class RegisterFailureCommand : CommandBase<Guid>
    {
        public Guid ResourceId { get; }
        public string ProblemDescription { get; }

        public RegisterFailureCommand(Guid resourceId, string problemDescription)
        {
            ResourceId = resourceId;
            ProblemDescription = problemDescription;
        }
    }
}
