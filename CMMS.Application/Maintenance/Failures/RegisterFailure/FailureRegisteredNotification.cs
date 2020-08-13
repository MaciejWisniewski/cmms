using CMMS.Application.Configuration.DomainEvents;
using CMMS.Domain.Maintenance.Failures;
using CMMS.Domain.Maintenance.Failures.Events;
using CMMS.Domain.Maintenance.Resources;
using Newtonsoft.Json;
using System;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class FailureRegisteredNotification : DomainNotificationBase<FailureRegisteredDomainEvent>
    {
        public FailureId FailureId { get; }
        public ResourceId ResourceId { get; }
        public string ResourceName { get; }
        public string ProblemDescription { get; }
        public DateTime FailureOccurredOn { get; }

        public FailureRegisteredNotification(FailureRegisteredDomainEvent domainEvent) : base(domainEvent)
        {
            FailureId = domainEvent.FailureId;
            ResourceId = domainEvent.ResourceId;
            ResourceName = domainEvent.ResourceName;
            ProblemDescription = domainEvent.ProblemDescription;
            FailureOccurredOn = domainEvent.FailureOccurredOn;
        }

        [JsonConstructor]
        public FailureRegisteredNotification(
            FailureId failureId,
            ResourceId resourceId,
            string resourceName,
            string problemDescription,
            DateTime failureOccurredOn
            ) : base(null)
        {
            FailureId = failureId;
            ResourceId = resourceId;
            ResourceName = resourceName;
            ProblemDescription = problemDescription;
            FailureOccurredOn = failureOccurredOn;
        }
    }
}
