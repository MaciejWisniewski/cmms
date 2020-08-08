using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Services.StartService
{
    public class StartServiceCommand : CommandBase
    {
        public Guid ServiceId { get; }
        public Guid ActualWorkerId { get; }
        public string Note { get; }

        public StartServiceCommand(Guid serviceId, Guid actualWorkerId, string note)
        {
            ServiceId = serviceId;
            ActualWorkerId = actualWorkerId;
            Note = note;
        }
    }
}
