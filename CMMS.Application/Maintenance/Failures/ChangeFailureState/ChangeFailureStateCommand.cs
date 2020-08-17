using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Failures.ChangeFailureState
{
    public class ChangeFailureStateCommand : CommandBase
    {
        public Guid FailureId { get; }
        public Guid WorkerId { get; }
        public string Note { get; }
        public string FailureState { get; }

        public ChangeFailureStateCommand(Guid failureId, Guid workerId, string note, string failureState)
        {
            FailureId = failureId;
            WorkerId = workerId;
            Note = note;
            FailureState = failureState;
        }
    }
}
