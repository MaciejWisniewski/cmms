using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Services.FinishService
{
    public class FinishServiceCommand : CommandBase
    {
        public Guid ServiceId { get; }
        public Guid FinishingWorkerId { get; }
        public string Note { get; }

        public FinishServiceCommand(Guid serviceId, Guid finishingWorkerId, string note)
        {
            ServiceId = serviceId;
            FinishingWorkerId = finishingWorkerId;
            Note = note;
        }
    }
}
