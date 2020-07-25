using CMMS.Domain.Maintenance.Resources.Events;
using CMMS.Domain.Maintenance.Resources.Rules;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Resources
{
    public class Resource : Entity, IAggregateRoot
    {
        public ResourceId Id { get; private set; }

        public ResourceId? ParentId { get; private set; }

        public string Name { get; private set; }

        public bool IsArea { get; private set; }

        public bool IsMachine { get; private set; }

        private Resource()
        {
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
            Name = name;
            IsArea = isArea;
            IsMachine = isMachine;

            AddDomainEvent(new ResourceCreatedDomainEvent(Id));
        }
    }
}
