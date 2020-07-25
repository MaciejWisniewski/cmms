using CMMS.Domain.Maintenance.Resources.Events;
using CMMS.Domain.Maintenance.Resources.Rules;
using CMMS.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace CMMS.Domain.Maintenance.Resources
{
    public class Resource : Entity, IAggregateRoot
    {
        public ResourceId Id { get; private set; }

        public ResourceId? ParentId { get; private set; }

        public List<Resource> Children { get; private set; }

        public string Name { get; private set; }

        public bool IsArea { get; private set; }

        public bool IsMachine { get; private set; }

        private Resource()
        {
            Children = new List<Resource>();
        }

        public static Resource CreateNew(
            Resource parent, 
            string name,
            bool isArea,
            bool isMachine)
        {
            return new Resource(parent, name, isArea, isMachine);
        }

        private Resource(Resource parent, string name, bool isArea, bool isMachine)
        {
            CheckRule(new ResourceCannotBeAreaAndMachineSimultaneously(isArea, isMachine));
            CheckRule(new MachineMustHaveAnAreaParent(isMachine, parent));
            CheckRule(new ParentCannotBeAMachine(parent));

            Id = new ResourceId(Guid.NewGuid());
            ParentId = parent?.Id;
            Children = new List<Resource>();
            Name = name;
            IsArea = isArea;
            IsMachine = isMachine;

            AddDomainEvent(new ResourceCreatedDomainEvent(Id));
        }

        public void Remove(Action<Resource> removeMethod)
        {
            CheckRule(new ParentResourceCannotBeRemoved(this));

            removeMethod(this);

            AddDomainEvent(new ResourceRemovedDomainEvent(Id));
        }
    }
}
