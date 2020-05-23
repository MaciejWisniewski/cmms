using CMMS.Domain.SeedWork;
using System;

namespace CMMS.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value) : base(value)
        {
        }
    }
}
