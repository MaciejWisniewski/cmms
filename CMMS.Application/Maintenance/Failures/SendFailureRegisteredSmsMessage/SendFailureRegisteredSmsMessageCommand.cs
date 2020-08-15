using CMMS.Application.Configuration.Commands;
using Newtonsoft.Json;
using System;

namespace CMMS.Application.Maintenance.Failures.SendFailureRegisteredSmsMessage
{
    internal class SendFailureRegisteredSmsMessageCommand : InternalCommandBase
    {
        public string ResourceName { get; }
        public DateTime FailureOccurredOn { get; }
        public string ProblemDescription { get; }
        public string ToPhoneNumber { get; }

        [JsonConstructor]
        internal SendFailureRegisteredSmsMessageCommand(
            Guid id,
            string resourceName,
            DateTime failureOccurredOn,
            string problemDescription,
            string toPhoneNumber) : base(id)
        {
            ResourceName = resourceName;
            FailureOccurredOn = failureOccurredOn;
            ProblemDescription = problemDescription;
            ToPhoneNumber = toPhoneNumber;
        }
    }
}
