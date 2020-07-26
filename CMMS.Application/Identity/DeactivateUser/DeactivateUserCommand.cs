using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Identity.DeactivateUser
{
    public class DeactivateUserCommand : CommandBase
    {
        public Guid UserId { get; }

        public DeactivateUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
