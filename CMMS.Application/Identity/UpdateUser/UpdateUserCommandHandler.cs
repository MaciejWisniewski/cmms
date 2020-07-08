using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            user.Update(request.FullName, request.Email, request.PhoneNumber);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
