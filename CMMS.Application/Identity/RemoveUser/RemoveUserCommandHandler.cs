using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.RemoveUser
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            await _userRepository.RemoveAsync(user);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
