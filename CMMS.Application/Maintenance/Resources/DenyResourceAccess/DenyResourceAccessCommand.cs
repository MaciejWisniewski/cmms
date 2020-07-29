using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Resources.DenyResourceAccess
{
    public class DenyResourceAccessCommand : CommandBase
    {
        public Guid ResourceId { get; }
        public Guid WorkerId { get; }

        public DenyResourceAccessCommand(Guid resourceId, Guid workerId)
        {
            ResourceId = resourceId;
            WorkerId = workerId;
        }
    }
}
