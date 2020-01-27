using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CMMS.DTOs;
using CMMS.Repositories;

namespace CMMS.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public AppUserService(IAppUserRepository repository, IMapper mapper)
        {
            _appUserRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUserDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<AppUserDto>>(await _appUserRepository.GetAllAsync());
        }
    }
}
