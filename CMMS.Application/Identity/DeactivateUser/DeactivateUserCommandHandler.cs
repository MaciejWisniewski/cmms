using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.DeactivateUser
{
    public class DeactivateUserCommandHandler : ICommandHandler<DeactivateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeactivateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            user.Deactivate();

            return Unit.Value;
        }
    }
}
