using CMMS.Domain.Maintenance.Failures.Events;
using CMMS.Domain.Maintenance.Failures.Rules;
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


        public void StartRepair(Worker worker)
        {
            CheckRule(new CannotStartAlreadyStartedOrFinishedRepairRule(State));

            WorkerId = worker.Id;
            State = FailureState.InProgress;

            AddDomainEvent(new FailureRepairStartedDomainEvent(
                Id,
                ResourceId,
                WorkerId,
                worker.UserName,
                State,
                ProblemDescription,
                Note,
                OccurredOn,
                ResolvedOn));
        }

        public void FinishRepair(Worker worker)
        {
            CheckRule(new StartAndFinishWorkerMustBeTheSameRule(WorkerId, worker.Id));
            CheckRule(new CannotFinishAlreadyFinishedRepairRule(State));

            State = FailureState.Resolved;
            ResolvedOn = DateTime.UtcNow;

            AddDomainEvent(new FailureRepairFinishedDomainEvent(
                Id,
                ResourceId,
                WorkerId,
                worker.UserName,
                State,
                ProblemDescription,
                Note,
                OccurredOn,
                ResolvedOn));
        }

        public void ChangeState(FailureState newfailureState, Worker worker, string note)
        {
            CheckRule(new StateCannotBeTheSameRule(State, newfailureState));

            State = newfailureState;
            WorkerId = worker.Id;
            Note = note;

            if(newfailureState == FailureState.Resolved)
            {
                ResolvedOn = DateTime.UtcNow;
            }

            AddDomainEvent(new FailureStateChangedDomainEvent(
                   Id,
                   ResourceId,
                   WorkerId,
                   worker.UserName,
                   State,
                   ProblemDescription,
                   Note,
                   OccurredOn,
                   ResolvedOn
                ));
        }
    }
}
