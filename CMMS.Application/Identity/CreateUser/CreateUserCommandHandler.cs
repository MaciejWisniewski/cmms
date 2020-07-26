using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserUniquenessChecker _userUniquenessChecker;
        private readonly IRoleValidator _roleValidator;

        public CreateUserCommandHandler(
            IUserRepository userRepository, 
            IUserUniquenessChecker userUniquenessChecker, 
            IRoleValidator roleValidator)
        {
            _userRepository = userRepository;
            _userUniquenessChecker = userUniquenessChecker;
            _roleValidator = roleValidator;
        }


        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var roleName = _roleValidator.GetValidOrDefault(command.RoleName);
            var user = AppUser.Create(
                fullName: command.FullName,
                userName: command.UserName,
                email: command.Email,
                phoneNumber: command.PhoneNumber,
                roleName: roleName,
                userUniquenessChecker: _userUniquenessChecker);

            await _userRepository.AddAsync(user, command.Password);
            await _userRepository.AddToRoleAsync(user, roleName);

            return user.Id;
        }
    }
}
