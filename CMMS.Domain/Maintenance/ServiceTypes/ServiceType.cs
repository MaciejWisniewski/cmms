using CMMS.Domain.SeedWork;

namespace CMMS.Domain.Maintenance.ServiceTypes
{
    public class ServiceType : Entity, IAggregateRoot
    {
        public ServiceTypeId Id { get; private set; }
        public string Name { get; private set; }

        private ServiceType()
        {
        }
    }
}
