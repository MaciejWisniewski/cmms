using CMMS.Domain.Maintenance.Failures.Events;
using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Failures
{
    public class Failure : Entity, IAggregateRoot
    {
        public FailureId Id { get; private set; }
        public ResourceId ResourceId { get; private set; }
        public WorkerId? WorkerId { get; private set; }
        public FailureState State { get; private set; }
        public string ProblemDescription { get; private set; }
        public string Note { get; private set; }
        public DateTime OccuredOn { get; private set; }
        public DateTime? ResolvedOn { get; private set; }

        private Failure()
        {
        }

        private Failure(ResourceId resourceId, string problemDescription)
        {
            Id = new FailureId(Guid.NewGuid());
            ResourceId = resourceId;
            ProblemDescription = problemDescription;
            State = FailureState.Detected;
            OccuredOn = DateTime.UtcNow;

            AddDomainEvent(new FailureRegisteredDomainEvent(Id));
        }

        public static Failure CreateNew(ResourceId resourceId, string problemDescription)
        {
            return new Failure(resourceId, problemDescription);
        }
    }
}
