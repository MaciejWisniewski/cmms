using MediatR;
using System.Collections.Generic;

namespace CMMS.Application.Identity.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<List<RoleDto>>
    {
    }
}
