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
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            var role = await _roleRepository.GetByNameAsync(request.RoleName);
            if (role == null)
                throw new NotFoundException("Role with the given name hasn't been found", null);

            user.Update(request.FullName, request.Email, request.PhoneNumber);
            var roles = await _userRepository.GetRolesAsync(user);
            await _userRepository.RemoveFromRolesAsync(user, roles);
            await _userRepository.AddToRoleAsync(user, role.Name);
            await _userRepository.ChangePasswordAsync(user, request.Password);

            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
