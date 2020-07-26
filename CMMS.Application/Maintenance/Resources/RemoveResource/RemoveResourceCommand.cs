using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Resources.RemoveResource
{
    public class RemoveResourceCommand : CommandBase
    {
        public Guid ResourceId { get; }

        public RemoveResourceCommand(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
