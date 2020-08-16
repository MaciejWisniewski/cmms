using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Failures.Rules
{
    public class CannotFinishAlreadyFinishedRepairRule : IBusinessRule
    {
        private readonly FailureState _failureState;

        public CannotFinishAlreadyFinishedRepairRule(FailureState failureState)
        {
            _failureState = failureState;
        }

        public bool IsBroken() => _failureState.Value == FailureState.Resolved.Value;
        
        public string Message => "Cannot finish already finished repair";
    }
}
