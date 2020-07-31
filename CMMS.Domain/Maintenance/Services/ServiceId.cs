using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Maintenance.Services
{
    public class ServiceId : TypedIdValueBase
    {
        public ServiceId(Guid value) : base(value)
        {
        }
    }
}
