using CMMS.Domain.Users;
using MediatR;

namespace CMMS.Application.Users
{
    public class MarkUserAsWelcomedCommand : IRequest
    {
        public MarkUserAsWelcomedCommand(UserId userId)
        {
            UserId = userId;
        }

        public UserId UserId { get; }
    }
}
