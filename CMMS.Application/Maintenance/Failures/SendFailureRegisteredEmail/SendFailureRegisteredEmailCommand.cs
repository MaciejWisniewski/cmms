using CMMS.Application.Configuration.Commands;
using Newtonsoft.Json;
using System;

namespace CMMS.Application.Maintenance.Failures.SendFailureRegisteredEmail
{
    internal class SendFailureRegisteredEmailCommand : InternalCommandBase
    {
        public string ResourceName { get; }
        public DateTime FailureOccurredOn { get; }
        public string ProblemDescription { get; }
        public string ToEmailAddress { get; }

        [JsonConstructor]
        internal SendFailureRegisteredEmailCommand(
            Guid id,
            string resourceName,
            DateTime failureOccurredOn,
            string problemDescription,
            string toEmailAddress) : base(id)
        {
            ResourceName = resourceName;
            FailureOccurredOn = failureOccurredOn;
            ProblemDescription = problemDescription;
            ToEmailAddress = toEmailAddress;
        }
    }
}
