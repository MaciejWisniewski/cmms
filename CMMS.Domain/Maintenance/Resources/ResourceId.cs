using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Resources
{
    public class ResourceId : TypedIdValueBase
    {
        public ResourceId(Guid value) : base(value)
        {
        }
    }
}
