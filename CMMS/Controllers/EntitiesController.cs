using System.Threading.Tasks;
using CMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : ControllerBase
    {
        private readonly IEntityService _entityService;

        public EntitiesController(IEntityService entityService)
        {
            _entityService = entityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var entityDtos = await _entityService.GetAllAsync();

            return Ok(entityDtos);
        }
    }
}