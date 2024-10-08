﻿using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.Resources.Events
{
    public class ResourceCreatedDomainEvent : DomainEventBase
    {
        public ResourceId ResourceId { get; }

        public ResourceCreatedDomainEvent(ResourceId resourceId)
        {
            ResourceId = resourceId;
        }

    }
}
