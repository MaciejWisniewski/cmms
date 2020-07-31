using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Failures.Events
{
    public class FailureRegisteredDomainEvent : DomainEventBase
    {
        public FailureId FailureId { get; }
        public ResourceId ResourceId { get; }
        public FailureState FailureState { get;  }
        public string ProblemDescription { get; }
        public DateTime OccurredOn { get;  }


        public FailureRegisteredDomainEvent(FailureId failureId, ResourceId resourceId, FailureState failureState, string problemDescription, DateTime occurredOn)
        {
            FailureId = failureId;
            ResourceId = resourceId;
            FailureState = failureState;
            ProblemDescription = problemDescription;
            OccurredOn = occurredOn;
        }
    }
}
