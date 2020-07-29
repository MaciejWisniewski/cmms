using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Resources.GiveResourceAccess
{
    public class GiveResourceAccessCommand : CommandBase
    {
        public Guid ResourceId { get; }
        public Guid WorkerId { get; }


        public GiveResourceAccessCommand(Guid resourceId, Guid workerId)
        {
            ResourceId = resourceId;
            WorkerId = workerId;
        }
    }
}
