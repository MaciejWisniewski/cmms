using CMMS.Domain.Maintenance.Resources;
using CMMS.Domain.Maintenance.Services.Events;
using CMMS.Domain.Maintenance.Services.Rules;
using CMMS.Domain.Maintenance.ServiceTypes;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services
{
    public class Service : Entity, IAggregateRoot
    {
        public ServiceId Id { get; private set; }
        public ResourceId ResourceId { get; private set; }
        public ServiceTypeId TypeId { get; private set; }
        public WorkerId ScheduledWorkerId { get; private set; }
        public WorkerId ActualWorkerId { get; private set; }
        public string Description { get; private set; } 
        public string Note { get; private set; }
        public DateTime ScheduledStartDateTime { get; private set; }
        public DateTime ScheduledEndDateTime { get; private set; }
        public DateTime? ActualStartDateTime { get; private set; }
        public DateTime? ActualEndDateTime { get; private set; }

        private Service()
        {
        }

        private Service(
            ResourceId resourceId, 
            ServiceTypeId typeId, 
            WorkerId scheduledWorkerId, 
            string description,
            DateTime scheduledStartDateTime,
            DateTime scheduledEndDateTime)
        {
            Id = new ServiceId(Guid.NewGuid());
            ResourceId = resourceId;
            TypeId = typeId;
            ScheduledWorkerId = scheduledWorkerId;
            Description = description;
            ScheduledStartDateTime = scheduledStartDateTime;
            ScheduledEndDateTime = scheduledEndDateTime;

            AddDomainEvent(new ServiceScheduledDomainEvent(Id));
        }

        public static Service Schedule(
            WorkerId schedulerId,
            Resource resource,
            ServiceTypeId typeId,
            WorkerId scheduledWorkerId,
            string description,
            DateTime scheduledStartDateTime,
            DateTime scheduledEndDateTime)
        {
            CheckRule(new WorkerMustHaveAccessToTheResourceToManageItsServicesRule(resource.Accesses, schedulerId));
            CheckRule(new ServiceCannotBeScheduledForAnAreaRule(resource));
            CheckRule(new WorkerMustHaveAccessToTheServicedResourceRule(resource.Accesses, scheduledWorkerId));
            CheckRule(new ServiceScheduledStartMustBeBeforeItsScheduledEndRule(scheduledStartDateTime, scheduledEndDateTime));

            return new Service(
                resource.Id, 
                typeId, 
                scheduledWorkerId, 
                description,
                scheduledStartDateTime,
                scheduledEndDateTime);
        }

        public void Start(Resource resource, WorkerId actualWorkerId, string note)
        {
            CheckRule(new WorkerMustHaveAccessToTheServicedResourceRule(resource.Accesses, actualWorkerId));
            CheckRule(new FinishedServiceCannotBeStartedNorFinishedAgainRule(ActualEndDateTime));
            CheckRule(new ServiceCanBeStartedOnceRule(ActualStartDateTime));

            ActualWorkerId = actualWorkerId;
            Note = note;
            ActualStartDateTime = DateTime.UtcNow;

            AddDomainEvent(new ServiceStartedDomainEvent(Id));
        }

        public void Finish(Resource resource, Worker finishingWorker, string note)
        {
            CheckRule(new WorkerMustHaveAccessToTheServicedResourceRule(resource.Accesses, finishingWorker.Id));
            CheckRule(new FinishedServiceCannotBeStartedNorFinishedAgainRule(ActualEndDateTime));
            CheckRule(new OnlyStartedServiceCanBeFinishedRule(ActualStartDateTime));
            CheckRule(new OnlyActualWorkerOrLeaderCanFinishServiceRule(ActualWorkerId, finishingWorker));

            Note = note;
            ActualEndDateTime = DateTime.UtcNow;

            AddDomainEvent(new ServiceFinishedDomainEvent(Id));
        }

        public void Edit(
            WorkerId editorId,
            Resource actualResource,
            Resource newResource,
            ServiceTypeId typeId,    
            WorkerId scheduledWorkerId,
            string description,
            DateTime scheduledStartDateTime,
            DateTime scheduledEndDateTime)
        {
            CheckRule(new WorkerMustHaveAccessToTheResourceToManageItsServicesRule(actualResource.Accesses, editorId));
            CheckRule(new WorkerMustHaveAccessToTheResourceToManageItsServicesRule(newResource.Accesses, editorId));
            CheckRule(new WorkerMustHaveAccessToTheServicedResourceRule(newResource.Accesses, scheduledWorkerId));
            CheckRule(new AlreadyStartedOrFinishedServiceCannotBeEditedRule(ActualStartDateTime, ActualEndDateTime));
            CheckRule(new ServiceScheduledStartMustBeBeforeItsScheduledEndRule(scheduledStartDateTime, scheduledEndDateTime));

            ResourceId = newResource.Id;
            TypeId = typeId;
            ScheduledWorkerId = scheduledWorkerId;
            Description = description;
            ScheduledStartDateTime = scheduledStartDateTime;
            ScheduledEndDateTime = scheduledEndDateTime;

            AddDomainEvent(new EditedScheduledServiceDomainEvent(Id));
        }

        public void Remove(Resource resource, WorkerId workerId, Action<Service> removeMethod)
        {
            CheckRule(new WorkerMustHaveAccessToTheServicedResourceRule(resource.Accesses, workerId));
            CheckRule(new AlreadyStartedOrFinishedServiceCannotBeRemovedRule(ActualStartDateTime, ActualEndDateTime));

            removeMethod(this);

            AddDomainEvent(new RemovedScheduledServiceDomainEvent(Id));
        }
    }
}
