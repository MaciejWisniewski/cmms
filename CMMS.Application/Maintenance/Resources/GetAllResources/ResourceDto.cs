using System;

namespace CMMS.Application.Maintenance.Resources.GetAllResources
{
    public class ResourceDto
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public bool? IsArea { get; set; }

        public bool? IsMachine { get; set; }
    }
}
