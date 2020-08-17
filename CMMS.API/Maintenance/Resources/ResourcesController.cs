using CMMS.Application.Maintenance.Resources.CreateResource;
using CMMS.Application.Maintenance.Resources.DenyResourceAccess;
using CMMS.Application.Maintenance.Resources.EditResource;
using CMMS.Application.Maintenance.Resources.GetAllResources;
using CMMS.Application.Maintenance.Resources.GetResourcesWorkerHasAccessTo;
using CMMS.Application.Maintenance.Resources.GiveResourceAccess;
using CMMS.Application.Maintenance.Resources.RemoveResource;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CMMS.API.Maintenance.Resources
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
        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(typeof(List<ResourceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllResources()
        {
            var resources = await _mediator.Send(new GetAllResourcesQuery());

            return Ok(resources);
        }

        /// <summary>
        /// Get all resources worker has access to.
        /// </summary>
        [HttpGet("all/{workerId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<ResourceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetResourcesWorkerHasAccessTo([FromRoute] Guid workerId)
        {
            var resources = await _mediator.Send(new GetResourcesWorkerHasAccessToQuery(workerId));

            return Ok(resources);
        }

        /// <summary>
        /// Create new resource.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateResource([FromBody] CreateResourceRequest request)
        {
            var resourceId = await _mediator.Send(new CreateResourceCommand(
                    request.ParentId,
                    request.Name,
                    request.IsArea,
                    request.IsMachine
                ));

            return Ok(resourceId);
        }

        /// <summary>
        /// Give resource access to the worker with the given id.
        /// </summary>
        [HttpPost("{resourceId}/resourceAccesses")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> GiveResourceAccess([FromRoute] Guid resourceId, [FromBody] GiveResourceAccessRequest request)
        {
            await _mediator.Send(new GiveResourceAccessCommand(resourceId, request.WorkerId));

            return Ok();
        }

        /// <summary>
        /// Deny resource access for the worker with the given id.
        /// </summary>
        [HttpDelete("{resourceId}/resourceAccesses")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> DenyResourceAccess([FromRoute] Guid resourceId, [FromBody] DenyResourceAccessRequest request)
        {
            await _mediator.Send(new DenyResourceAccessCommand(resourceId, request.WorkerId));

            return Ok();
        }

        /// <summary>
        /// Edit resource with the given id.
        /// </summary>
        [HttpPut("{resourceId}")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> EditResource([FromRoute] Guid resourceId, [FromBody] EditResourceRequest request)
        {
            await _mediator.Send(new EditResourceCommand(resourceId, request.Name));

            return Ok();
        }

        /// <summary>
        /// Remove resource with the given id.
        /// </summary>
        [HttpDelete("{resourceId}")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RemoveResource([FromRoute] Guid resourceId)
        {
            await _mediator.Send(new RemoveResourceCommand(resourceId));

            return Ok();
        }
    }
}
