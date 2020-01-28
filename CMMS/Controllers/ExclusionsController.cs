using System.Threading.Tasks;
using CMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExclusionsController : ControllerBase
    {
        private readonly IExclusionService _exclusionService;
        private readonly IEntityService _entityService;

        public ExclusionsController(IExclusionService exclusionService, IEntityService entityService)
        {
            _exclusionService = exclusionService;
            _entityService = entityService;
        }

        [HttpGet("{entityId}")]
        public async Task<IActionResult> GetByEntityIdAsync(int entityId)
        {
            if (!await _entityService.EntityExistsAsync(entityId))
                return NotFound("Entity with the given id doesn't exist");

            var exclusionDtos = await _exclusionService.GetByEntityIdAsync(entityId);

            return Ok(exclusionDtos);
        }
    }
}