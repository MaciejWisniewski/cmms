﻿using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserUniquenessChecker _userUniquenessChecker;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUserUniquenessChecker userUniquenessChecker, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userUniquenessChecker = userUniquenessChecker;
            _unitOfWork = unitOfWork;
        }


        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = AppUser.Create(
                fullName: request.FullName,
                userName: request.UserName,
                email: request.Email,
                phoneNumber: request.PhoneNumber,
                _userUniquenessChecker
                );

            await _userRepository.AddAsync(user, request.Password);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new UserDto() { Id = user.Id };
        }
    }
}
