using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.ChangeUserRole
{
    public class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeUserRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role == null)
                throw new NotFoundException("Role with the given id hasn't been found", null);

            var roles = await _userRepository.GetRolesAsync(user);
            await _userRepository.RemoveFromRolesAsync(user, roles);
            await _userRepository.AddToRoleAsync(user, role.Name);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
