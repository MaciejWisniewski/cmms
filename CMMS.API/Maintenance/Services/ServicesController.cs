﻿using CMMS.Application.Maintenance.Services.EditScheduledService;
using CMMS.Application.Maintenance.Services.FinishService;
using CMMS.Application.Maintenance.Services.ScheduleService;
using CMMS.Application.Maintenance.Services.StartService;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var serviceId = await _mediator.Send(new ScheduleServiceCommand(
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
            await _mediator.Send(new EditScheduledServiceCommand(
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
            await _mediator.Send(new StartServiceCommand(serviceId, request.ActualWorkerId, request.Note));

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
            await _mediator.Send(new FinishServiceCommand(serviceId, request.FinishingWorkerId, request.Note));

            return Ok();
        }
    }
}
