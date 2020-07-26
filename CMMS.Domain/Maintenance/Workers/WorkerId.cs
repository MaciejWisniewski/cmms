using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Workers
{
    public class WorkerId : TypedIdValueBase
    {
        public WorkerId(Guid value) : base(value)
        {
        }
    }
}
