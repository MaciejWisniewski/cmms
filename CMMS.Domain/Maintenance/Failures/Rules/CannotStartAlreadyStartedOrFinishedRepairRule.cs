using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Failures.Rules
{
    public class CannotStartAlreadyStartedOrFinishedRepairRule : IBusinessRule
    {
        private readonly FailureState _failureState;

        public CannotStartAlreadyStartedOrFinishedRepairRule(FailureState failureState)
        {
            _failureState = failureState;
        }

        public bool IsBroken() => _failureState.Value == FailureState.InProgress.Value || 
            _failureState.Value == FailureState.Resolved.Value;
        
        public string Message => "Cannot start already started or finished repair";

    }
}
