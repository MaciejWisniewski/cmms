using MediatR;
using System;

namespace CMMS.Application.Maintenance.Resources.RemoveResource
{
    public class RemoveResourceCommand : IRequest
    {
        public Guid ResourceId { get; }

        public RemoveResourceCommand(Guid resourceId)
        {
            ResourceId = resourceId;
        }
    }
}
