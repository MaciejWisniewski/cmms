using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Maintenance.ServiceTypes.AddServiceType
{
    public class AddServiceTypeCommand : CommandBase<Guid>
    {
        public string Name { get; }

        public AddServiceTypeCommand(string name)
        {
            Name = name;
        }
    }
}
