using CMMS.Application.Configuration.Commands;
using System;

namespace CMMS.Application.Identity.ChangeUserRole
{
    public class ChangeUserRoleCommand : CommandBase
    {
        public Guid UserId { get; }

        public Guid RoleId { get; }

        public ChangeUserRoleCommand(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }
}
