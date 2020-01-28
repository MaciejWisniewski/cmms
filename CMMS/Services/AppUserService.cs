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

        public async Task<IEnumerable<AppUserDto>> GetAllAsync()
        {
            var users = await _appUserRepository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<AppUserDto>>(users);

            //TODO: Dwell on the issue of getting users with their roles, this solution may cause some performance issues
            foreach (var userDto in userDtos)
                userDto.Role = (await _userManager.GetRolesAsync(users.Single(u => u.UserName == userDto.UserName))).FirstOrDefault();

            return userDtos;
        }
    }
}
