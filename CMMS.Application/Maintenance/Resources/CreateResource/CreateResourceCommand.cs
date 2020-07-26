using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.Resources.CreateResource
{
    public class CreateResourceCommand : CommandBase<Guid>
    {
        public Guid? ParentId { get; }

        public string Name { get; }

        public bool IsArea { get; }

        public bool IsMachine { get; }

        public CreateResourceCommand(Guid? parentId, string name, bool isArea, bool isMachine)
        {
            ParentId = parentId;
            Name = name;
            IsArea = isArea;
            IsMachine = isMachine;
        }
    }
}
