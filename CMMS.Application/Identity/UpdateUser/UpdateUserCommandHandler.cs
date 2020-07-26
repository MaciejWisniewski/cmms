using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Identity;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.UpdateUser
{
    public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            var role = await _roleRepository.GetByNameAsync(command.RoleName);
            if (role == null)
                throw new NotFoundException("Role with the given name hasn't been found", null);

            var roles = await _userRepository.GetRolesAsync(user);
            user.Update(command.FullName, command.Email, command.PhoneNumber, role.Name);
            await _userRepository.RemoveFromRolesAsync(user, roles);
            await _userRepository.AddToRoleAsync(user, role.Name);
            await _userRepository.ChangePasswordAsync(user, command.Password);

            return Unit.Value;
        }
    }
}
