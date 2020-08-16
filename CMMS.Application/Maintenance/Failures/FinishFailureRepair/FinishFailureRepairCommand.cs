using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Failures.FinishFailureRepair
{
    public class FinishFailureRepairCommand : CommandBase
    {
        public Guid FailureId { get; }
        public Guid WorkerId { get; }

        public FinishFailureRepairCommand(Guid failureId, Guid workerId)
        {
            FailureId = failureId;
            WorkerId = workerId;
        }
    }
}
