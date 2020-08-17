using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Failures.StartFailureRepair
{
    public class StartFailureRepairCommand : CommandBase
    {
        public Guid FailureId { get; }
        public Guid WorkerId { get; }

        public StartFailureRepairCommand(Guid failureId, Guid workerId)
        {
            FailureId = failureId;
            WorkerId = workerId;
        }
    }
}
