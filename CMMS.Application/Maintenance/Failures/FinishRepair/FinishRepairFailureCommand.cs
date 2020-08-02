using CMMS.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMMS.Application.Maintenance.Failures.FinishRepair
{
    public class FinishRepairFailureCommand : CommandBase
    {
        public Guid FailureId { get; }
        public Guid WorkerId { get; }

        public FinishRepairFailureCommand(Guid failureId, Guid workerId)
        {
            FailureId = failureId;
            WorkerId = workerId;
        }
    }
}
