using System;

namespace CMMS.Application.Maintenance.Resources.EditResource
{
    public class EditResourceRequest
    {
        public Guid? ParentId { get; set; }

        public string Name { get; set; }
    }
}
