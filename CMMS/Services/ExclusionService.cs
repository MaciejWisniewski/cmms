using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CMMS.DTOs;
using CMMS.Repositories;

namespace CMMS.Services
{
    public class ExclusionService : IExclusionService
    {
        private readonly IExclusionRepository _exclusionRepository;
        private readonly IMapper _mapper;

        public ExclusionService(IExclusionRepository exclusionRepository, IMapper mapper)
        {
            _exclusionRepository = exclusionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExclusionDto>> GetByEntityIdAsync(int entityId)
        {
            return _mapper.Map<IEnumerable<ExclusionDto>>(await _exclusionRepository.GetByEntityIdAsync(entityId));
        }
    }
}
