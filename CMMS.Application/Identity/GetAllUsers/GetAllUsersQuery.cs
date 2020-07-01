using MediatR;
using System.Collections.Generic;

namespace CMMS.Application.Identity.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}
