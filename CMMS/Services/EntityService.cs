using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CMMS.DTOs;
using CMMS.Repositories;

namespace CMMS.Services
{
    public class EntityService : IEntityService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IMapper _mapper;

        public EntityService(IEntityRepository entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EntityDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<EntityDto>>(await _entityRepository.GetAllAsync());
        }
    }
}
