using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMMS.DTOs;
using CMMS.Models;
using CMMS.Repositories;
using Microsoft.AspNetCore.Identity;

namespace CMMS.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(IAppUserRepository repository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _appUserRepository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<AppUserDto> GetByUserNameAsync(string userName)
        {
            var user = await _appUserRepository.GetByUserNameAsync(userName);
            var userDto = _mapper.Map<AppUserDto>(user);
            userDto.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return userDto;
        }

        public async Task<IEnumerable<AppUserDto>> GetAllAsync()
        {
            var users = await _appUserRepository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<AppUserDto>>(users);

            //TODO: Create userDtos one by one and append their roles then
            foreach (var userDto in userDtos)
                userDto.Role = (await _userManager.GetRolesAsync(users.Single(u => u.UserName == userDto.UserName))).FirstOrDefault();

            return userDtos;
        }

        public async Task<IdentityResult> CreateAsync(AppUserDto userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);
            var identityResult = await _userManager.CreateAsync(user, userDto.Password);

            return identityResult;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppUserDto userDto)
        {
            var user = await _appUserRepository.GetByUserNameAsync(userDto.UserName);
            var identityResult = await _userManager.AddToRoleAsync(user, userDto.Role);

            return identityResult;
        }
    }
}
