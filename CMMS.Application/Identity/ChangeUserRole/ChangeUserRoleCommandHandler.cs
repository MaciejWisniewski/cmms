using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.ChangeUserRole
{
    public class ChangeUserRoleCommandHandler : ICommandHandler<ChangeUserRoleCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public ChangeUserRoleCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(ChangeUserRoleCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            var role = await _roleRepository.GetByIdAsync(command.RoleId);
            if (role == null)
                throw new NotFoundException("Role with the given id hasn't been found", null);

            var roles = await _userRepository.GetRolesAsync(user);
            await _userRepository.RemoveFromRolesAsync(user, roles);
            await _userRepository.AddToRoleAsync(user, role.Name);
            user.ChangeRole(role);

            return Unit.Value;
        }
    }
}
