using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserUniquenessChecker _userUniquenessChecker;
        private readonly IRoleValidator _roleValidator;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(
            IUserRepository userRepository, 
            IUserUniquenessChecker userUniquenessChecker, 
            IRoleValidator roleValidator,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userUniquenessChecker = userUniquenessChecker;
            _roleValidator = roleValidator;
            _unitOfWork = unitOfWork;
        }


        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = AppUser.Create(
                fullName: request.FullName,
                userName: request.UserName,
                email: request.Email,
                phoneNumber: request.PhoneNumber,
                _userUniquenessChecker);

            await _userRepository.AddAsync(user, request.Password);

            await _userRepository.AddToRoleAsync(user, _roleValidator.GetValidOrDefault(request.Role));

            await _unitOfWork.CommitAsync(cancellationToken);

            return user.Id;
        }
    }
}
