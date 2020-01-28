using System.Threading.Tasks;
using CMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExclusionTypesController : ControllerBase
    {
        private readonly IExclusionTypeService _exclusionTypeService;

        public ExclusionTypesController(IExclusionTypeService exclusionTypeService)
        {
            _exclusionTypeService = exclusionTypeService;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var exclusionTypeDtos = await _exclusionTypeService.GetAllAsync();

            return Ok(exclusionTypeDtos);
        }
    }
}