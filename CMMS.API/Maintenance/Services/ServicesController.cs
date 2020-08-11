using CMMS.API.Configuration;
using CMMS.Application.Maintenance.Services.EditScheduledService;
using CMMS.Application.Maintenance.Services.FinishService;
using CMMS.Application.Maintenance.Services.GetServicesByWorkerAccesses;
using CMMS.Application.Maintenance.Services.RemoveScheduledService;
using CMMS.Application.Maintenance.Services.ScheduleService;
using CMMS.Application.Maintenance.Services.StartService;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CMMS.API.Maintenance.Services
{
    [Route("api/maintenance/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<ServiceDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetServiceByWorkerAccesses()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var services = await _mediator.Send(new GetServicesByWorkerAccessesQuery(JwtTokenHelper.ExtractUserId(accessToken)));

            return Ok(services);
        }




        /// <summary>
        /// Schedule a service.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = UserRole.Leader)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> ScheduleService([FromBody]ScheduleServiceRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var serviceId = await _mediator.Send(new ScheduleServiceCommand(
                    JwtTokenHelper.ExtractUserId(accessToken),
                    request.ResourceId,
                    request.ServiceTypeId,
                    request.ScheduledWorkerId,
                    request.Description,
                    request.ScheduledStartDateTime,
                    request.ScheduledEndDateTime
                ));

            return Ok(serviceId);
        }

        /// <summary>
        /// Edit scheduled service.
        /// </summary>
        [HttpPut("{serviceId}")]
        [Authorize(Roles = UserRole.Leader)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> EditScheduledService([FromRoute]Guid serviceId, [FromBody]EditScheduledServiceRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _mediator.Send(new EditScheduledServiceCommand(
                JwtTokenHelper.ExtractUserId(accessToken),
                serviceId,
                request.ResourceId,
                request.ServiceTypeId,
                request.ScheduledWorkerId,
                request.Description,
                request.ScheduledStartDateTime,
                request.ScheduledEndDateTime));

            return Ok();
        }

        /// <summary>
        /// Remove scheduled service.
        /// </summary>
        [HttpDelete("{serviceId}")]
        [Authorize(Roles = UserRole.Leader)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> RemoveScheduledService([FromRoute]Guid serviceId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _mediator.Send(new RemoveScheduledServiceCommand(
                JwtTokenHelper.ExtractUserId(accessToken), 
                serviceId));

            return Ok();
        }

        /// <summary>
        /// Start a service.
        /// </summary>
        [HttpPatch("{serviceId}/start")]
        [Authorize(Roles = UserRole.User)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> StartService([FromRoute]Guid serviceId, [FromBody]StartServiceRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _mediator.Send(new StartServiceCommand(
                serviceId, 
                JwtTokenHelper.ExtractUserId(accessToken), 
                request.Note));

            return Ok();
        }

        /// <summary>
        /// Finish a service.
        /// </summary>
        [HttpPatch("{serviceId}/finish")]
        [Authorize(Roles = UserRole.UserOrLeader)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> FinishService([FromRoute]Guid serviceId, [FromBody]FinishServiceRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _mediator.Send(new FinishServiceCommand(
                serviceId,
                JwtTokenHelper.ExtractUserId(accessToken),
                request.Note));

            return Ok();
        }
    }
}
