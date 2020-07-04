using MediatR;
using System;

namespace CMMS.Application.Identity.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public Guid Id { get; }

        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
