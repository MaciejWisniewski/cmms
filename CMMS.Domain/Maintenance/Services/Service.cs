using CMMS.Domain.Maintenance.Resources;
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

        public static Service CreateNew()
        {
            return new Service();
        }
    }
}
