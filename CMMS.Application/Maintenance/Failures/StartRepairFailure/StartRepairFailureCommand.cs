using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Failures.StartRepairFailure
{
    public class StartRepairFailureCommand : CommandBase
    {
        public Guid FailureId { get; }
        public Guid WorkerId { get; }

        public StartRepairFailureCommand(Guid failureId, Guid workerId)
        {
            FailureId = failureId;
            WorkerId = workerId;
        }

    }
}
