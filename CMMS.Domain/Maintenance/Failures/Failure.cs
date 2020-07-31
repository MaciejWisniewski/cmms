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
        public DateTime OccurredOn { get; private set; }
        public DateTime? ResolvedOn { get; private set; }

        private Failure()
        {
        }

        private Failure(Resource resource, string problemDescription)
        {
            Id = new FailureId(Guid.NewGuid());
            ResourceId = resource.Id;
            ProblemDescription = problemDescription;
            State = FailureState.Detected;
            OccurredOn = DateTime.UtcNow;

            AddDomainEvent(new FailureRegisteredDomainEvent(
                Id, 
                ResourceId, 
                resource.Name,
                State, 
                ProblemDescription, 
                OccurredOn));
        }

        public static Failure CreateNew(Resource resource, string problemDescription)
        {
            return new Failure(resource, problemDescription);
        }
    }
}
