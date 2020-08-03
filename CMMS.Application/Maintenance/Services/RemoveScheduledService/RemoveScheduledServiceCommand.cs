using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Services.RemoveScheduledService
{
    public class RemoveScheduledServiceCommand : CommandBase
    {
        public Guid WorkerId { get; }
        public Guid ServiceId { get; }

        public RemoveScheduledServiceCommand(Guid workerId, Guid serviceId)
        {
            WorkerId = workerId;
            ServiceId = serviceId;
        }
    }
}
