using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CMMS.DTOs;
using CMMS.Repositories;

namespace CMMS.Services
{
    public class DivisionService : IDivisionService
    {
        private readonly IDivisionRepository _divisionRepository;
        private readonly IMapper _mapper;

        public DivisionService(IDivisionRepository divisionRepository, IMapper mapper)
        {
            _divisionRepository = divisionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DivisionDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<DivisionDto>>(await _divisionRepository.GetAllAsync());
        }
    }
}
