using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Resources.EditResource
{
    public class EditResourceCommand : CommandBase
    {
        public Guid ResourceId { get;  }

        public Guid? ParentId { get; }

        public string Name { get; }

        public EditResourceCommand(Guid resourceId, Guid? parentId, string name)
        {
            ResourceId = resourceId;
            ParentId = parentId;
            Name = name;
        }
    }
}
