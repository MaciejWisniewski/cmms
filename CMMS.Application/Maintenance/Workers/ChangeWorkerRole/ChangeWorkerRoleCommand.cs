using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Workers.ChangeWorkerRole
{
    public class ChangeWorkerRoleCommand : CommandBase
    {
        public Guid WorkerId { get; }
        public string Role { get; }

        public ChangeWorkerRoleCommand(Guid workerId, string role)
        {
            WorkerId = workerId;
            Role = role;
        }
    }
}
