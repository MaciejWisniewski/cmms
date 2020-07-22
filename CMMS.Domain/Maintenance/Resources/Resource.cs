using CMMS.Domain.Maintenance.Resources.Events;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Resources
{
    public class Resource : Entity, IAggregateRoot
    {
        public ResourceId Id { get; private set; }

        public ResourceId ParentId { get; private set; }

        public string Name { get; private set; }

        public bool? IsArea { get; private set; }

        public bool? IsMachine { get; private set; }

        private Resource()
        {
        }

        internal static Resource CreateNew(
            ResourceId parentId, 
            string name,
            bool? isArea,
            bool? isMachine)
        {
            return new Resource(parentId, name, isArea, isMachine);
        }

        private Resource(ResourceId parentId, string name, bool? isArea, bool? isMachine)
        {
            Id = new ResourceId(Guid.NewGuid());
            ParentId = parentId;
            Name = name;
            IsArea = isArea;
            IsMachine = isMachine;

            AddDomainEvent(new ResourceCreatedDomainEvent(Id));
        }
    }
}
