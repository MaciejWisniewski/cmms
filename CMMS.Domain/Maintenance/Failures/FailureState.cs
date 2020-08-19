using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Failures
{
    public class FailureState : ValueObject
    {
        public static FailureState Detected => new FailureState("Detected");
        public static FailureState InProgress => new FailureState("InProgress");
        public static FailureState Resolved => new FailureState("Resolved");


        public string Value { get; }

        private FailureState(string value)
        {
            Value = value;
        }
    }
}
