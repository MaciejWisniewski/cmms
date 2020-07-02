using MediatR;

namespace CMMS.Application.Identity.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public string UserName { get; }

        public GetUserQuery(string userName)
        {
            UserName = userName;
        }
    }
}
