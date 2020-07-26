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


        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = AppUser.Create(
                fullName: request.FullName,
                userName: request.UserName,
                email: request.Email,
                phoneNumber: request.PhoneNumber,
                userUniquenessChecker: _userUniquenessChecker);

            await _userRepository.AddAsync(user, request.Password);
            await _userRepository.AddToRoleAsync(user, _roleValidator.GetValidOrDefault(request.Role));

            return user.Id;
        }
    }
}
