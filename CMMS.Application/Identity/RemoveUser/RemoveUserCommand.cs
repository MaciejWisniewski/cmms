using MediatR;
using System;

namespace CMMS.Application.Identity.RemoveUser
{
    public class RemoveUserCommand : IRequest
    {
        public Guid Id { get; }

        public RemoveUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
