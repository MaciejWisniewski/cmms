using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CMMS.DTOs;
using CMMS.Repositories;

namespace CMMS.Services
{
    public class ExclusionTypeService : IExclusionTypeService
    {
        private readonly IExclusionTypeRepository _exclusionTypeRepository;
        private readonly IMapper _mapper;

        public ExclusionTypeService(IExclusionTypeRepository exclusionTypeRepository, IMapper mapper)
        {
            _exclusionTypeRepository = exclusionTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExclusionTypeDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ExclusionTypeDto>>(await _exclusionTypeRepository.GetAllAsync());
        }
    }
}
