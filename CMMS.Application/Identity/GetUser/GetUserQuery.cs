using CMMS.Application.Configuration.Queries;
using System;

namespace CMMS.Application.Identity.GetUser
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public Guid Id { get; }

        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
