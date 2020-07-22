using System;

namespace CMMS.Application.Maintenance.Resources.CreateResource
{
    public class CreateResourceRequest
    {
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public bool? IsArea { get; set; }

        public bool? IsMachine { get; set; }
    }
}
