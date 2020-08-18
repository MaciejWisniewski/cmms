using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Resources.EditResource
{
    public class EditResourceCommand : CommandBase
    {
        public Guid ResourceId { get; }

        public string Name { get; }

        public EditResourceCommand(Guid resourceId, string name)
        {
            ResourceId = resourceId;
            Name = name;
        }
    }
}
