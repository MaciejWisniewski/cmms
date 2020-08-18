using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Failures.Events
{
    public class FailureRegisteredDomainEvent : DomainEventBase
    {
        public FailureId FailureId { get; }
        public ResourceId ResourceId { get; }
        public string ResourceName { get; }
        public FailureState FailureState { get; }
        public string ProblemDescription { get; }
        public DateTime FailureOccurredOn { get; }


        public FailureRegisteredDomainEvent(
            FailureId failureId,
            ResourceId resourceId,
            string resourceName,
            FailureState failureState,
            string problemDescription,
            DateTime failureOccurredOn)
        {
            FailureId = failureId;
            ResourceId = resourceId;
            ResourceName = resourceName;
            FailureState = failureState;
            ProblemDescription = problemDescription;
            FailureOccurredOn = failureOccurredOn;
        }
    }
}
