using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Failures.Rules
{
    public class OnlyStartedRepairCanBeFinishedRule : IBusinessRule
    {
        private readonly FailureState _failureState;

        public OnlyStartedRepairCanBeFinishedRule(FailureState failureState)
        {
            _failureState = failureState;
        }

        public bool IsBroken() => _failureState.Value != FailureState.InProgress.Value;

        public string Message => "Only started repair can be finished";
    }
}
