using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Failures.Events
{
    public class FailureRepairFinishedDomainEvent : DomainEventBase
    {
        public FailureId FailureId { get; }
        public ResourceId ResourceId { get; }
        public WorkerId WorkerId { get; }
        public string WorkerUserName { get; }
        public FailureState FailureState { get; }
        public string ProblemDescription { get; }
        public string Note { get; }
        public DateTime FailureOccurredOn { get; }
        public DateTime? ResolvedOn { get; }

        public FailureRepairFinishedDomainEvent(FailureId failureId, ResourceId resourceId, WorkerId workerId, string workerUserName, FailureState failureState, string problemDescription, string note, DateTime failureOccurredOn, DateTime? resolvedOn)
        {
            FailureId = failureId;
            ResourceId = resourceId;
            WorkerId = workerId;
            WorkerUserName = workerUserName;
            FailureState = failureState;
            ProblemDescription = problemDescription;
            Note = note;
            FailureOccurredOn = failureOccurredOn;
            ResolvedOn = resolvedOn;
        }
    }
}
