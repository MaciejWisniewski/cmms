using CMMS.Application.Configuration.Validation;
using CMMS.Domain.Identity;
using CMMS.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.ChangeUserRole
{
    public class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeUserRoleCommandHandler(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role == null)
                throw new NotFoundException("Role with the given id hasn't been found", null);

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, role.Name);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
