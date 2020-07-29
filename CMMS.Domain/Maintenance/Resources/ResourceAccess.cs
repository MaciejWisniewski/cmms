using CMMS.Domain.Maintenance.Resources.Events;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Resources
{
    public class ResourceAccess : Entity
    {
        public ResourceId ResourceId { get; private set; }
        public WorkerId WorkerId { get; private set; }
        public DateTime GivenOn { get; private set; }
        public virtual Worker Worker { get; } //Only for EF configuration purposes

        private ResourceAccess()
        {
            GivenOn = DateTime.Now;
        }

        private ResourceAccess(ResourceId resourceId, WorkerId workerId)
        {
            ResourceId = resourceId;
            WorkerId = workerId;
            GivenOn = DateTime.Now;

            AddDomainEvent(new GaveResourceAccessDomainEvent(ResourceId, WorkerId));
        }

        internal static ResourceAccess CreateNew(ResourceId resourceId, WorkerId workerId)
        {
            return new ResourceAccess(resourceId, workerId);
        }
        
    }
}
