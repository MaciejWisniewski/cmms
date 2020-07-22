using CMMS.Application.Maintenance.Resources.GetAllResources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CMMS.API.Maintenance
{
    [Route("api/maintenance/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all resources.
        /// </summary>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<ResourceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _mediator.Send(new GetAllResourcesQuery());

            return Ok(resources);
        }
    }
}
