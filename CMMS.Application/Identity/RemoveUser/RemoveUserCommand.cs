using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Identity.RemoveUser
{
    public class RemoveUserCommand : CommandBase
    {
        public Guid UserId { get; }

        public RemoveUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
