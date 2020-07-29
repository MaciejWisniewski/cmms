using CMMS.Domain.Maintenance.Resources.Events;
using CMMS.Domain.Maintenance.Resources.Rules;
using CMMS.Domain.Maintenance.Workers;
using CMMS.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMMS.Domain.Maintenance.Resources
{
    public class Resource : Entity, IAggregateRoot
    {
        public ResourceId Id { get; private set; }
        public ResourceId? ParentId { get; private set; }
        public virtual Resource Parent { get; private set; }
        public List<Resource> Children { get; private set; }
        public string Name { get; private set; }
        public bool IsArea { get; private set; }
        public bool IsMachine { get; private set; }
        public List<ResourceAccess> Accesses { get; private set; }

        private Resource()
        {
            Children = new List<Resource>();
            Accesses = new List<ResourceAccess>();
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
            Accesses = new List<ResourceAccess>();

            AddDomainEvent(new ResourceCreatedDomainEvent(Id));
        }

        public static Resource CreateNew(
            Resource parent, 
            string name,
            bool isArea,
            bool isMachine)
        {
            return new Resource(parent, name, isArea, isMachine);
        }

        public void Edit(string name)
        {
            Name = name;

            AddDomainEvent(new ResourceEditedDomainEvent(Id));
        }

        public void GiveAccess(WorkerId workerId)
        {
            CheckRule(new ResourceAccessCannotBeGivenTwice(this, workerId));

            Accesses.Add(ResourceAccess.CreateNew(Id, workerId));
            GiveAccessToAllDescendants(workerId);
            UpdateAncestorsAccesses(Parent, workerId);
        }

        private void GiveAccessToAllDescendants(WorkerId workerId)
        {
            foreach (var resource in this.Descendants())
                if (!HasAccess(resource, workerId))
                    resource.Accesses.Add(ResourceAccess.CreateNew(resource.Id, workerId));
        }

        private void UpdateAncestorsAccesses(Resource parent, WorkerId workerId)
        {
            bool hasAccessToAllChildren;
            do
            {
                hasAccessToAllChildren = parent != null && parent.Children.All(c => HasAccess(c, workerId));
                if (hasAccessToAllChildren)
                    parent.Accesses.Add(ResourceAccess.CreateNew(parent.Id, workerId));

                parent = parent.Parent;
            } while (hasAccessToAllChildren);
        }

        private bool HasAccess(Resource resource, WorkerId workerId)
        {
            return resource.Accesses.FirstOrDefault(a => a.WorkerId == workerId) != null;
        }

        public void Remove(Action<Resource> removeMethod)
        {
            CheckRule(new ParentResourceCannotBeRemoved(this));

            removeMethod(this);

            AddDomainEvent(new ResourceRemovedDomainEvent(Id));
        }
    }
}
