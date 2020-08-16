using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Failures.FinishFailureRepair
{
    public class FinishFailureRepairCommand : CommandBase
    {
        public Guid FailureId { get; }
        public Guid WorkerId { get; }
        public string Note { get; }

        public FinishFailureRepairCommand(Guid failureId, Guid workerId, string note)
        {
            FailureId = failureId;
            WorkerId = workerId;
            Note = note;
        }
    }
}
