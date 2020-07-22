using CMMS.Application.Maintenance.Resources.CreateResource;
using CMMS.Application.Maintenance.Resources.GetAllResources;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        /// <summary>
        /// Create new resource.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = UserRole.Leader)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateUser([FromBody] CreateResourceRequest request)
        {
            var resourceId = await _mediator.Send(new CreateResourceCommand(
                    request.ParentId,
                    request.Name,
                    request.IsArea,
                    request.IsMachine
                ));

            return Ok(resourceId);
        }
    }
}
