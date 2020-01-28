using System.Threading.Tasks;
using CMMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : ControllerBase
    {
        private readonly IDivisionService _divisionService;

        public DivisionsController(IDivisionService divisionService)
        {
            _divisionService = divisionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var divisionDtos = await _divisionService.GetAllAsync();

            return Ok(divisionDtos);
        } 
    }
}