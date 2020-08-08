using CMMS.Domain.Maintenance.ServiceTypes.Events;
using CMMS.Domain.Maintenance.ServiceTypes.Rules;
using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.ServiceTypes
{
    public class ServiceType : Entity, IAggregateRoot
    {
        public ServiceTypeId Id { get; private set; }
        public string Name { get; private set; }

        private ServiceType()
        {
        }

        private ServiceType(string name)
        {
            Id = new ServiceTypeId(Guid.NewGuid());
            Name = name;

            AddDomainEvent(new ServiceTypeAddedDomainEvent(Id));
        }

        public static ServiceType CreateNew(string name, IServiceTypeUniquenessChecker uniquenessChecker)
        {
            CheckRule(new ServiceTypeMustHaveUniqueNameRule(name, uniquenessChecker));

            return new ServiceType(name);
        }
    }
}
