using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CMMS.Application.Areas.GetAreas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Areas
{
    [Route("api/areas")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all areas.
        /// </summary>
        /// <returns>List of areas.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AreaDto>), (int)HttpStatusCode.OK)]
        
        public async Task<IActionResult> GetAllAreas()
        {
            var areas = await _mediator.Send(new GetAllAreasQuery());

            return Ok(areas);
        }
    }
}