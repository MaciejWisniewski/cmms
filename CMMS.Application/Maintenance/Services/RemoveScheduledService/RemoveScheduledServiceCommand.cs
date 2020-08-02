using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Services.RemoveScheduledService
{
    public class RemoveScheduledServiceCommand : CommandBase
    {
        public Guid ServiceId { get; }

        public RemoveScheduledServiceCommand(Guid serviceId)
        {
            ServiceId = serviceId;
        }
    }
}
