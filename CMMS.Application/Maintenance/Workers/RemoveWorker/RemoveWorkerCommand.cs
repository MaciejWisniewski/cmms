using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Workers.RemoveWorker
{
    public class RemoveWorkerCommand : CommandBase
    {
        public Guid WorkerId { get; }

        public RemoveWorkerCommand(Guid workerId)
        {
            WorkerId = workerId;
        }
    }
}
