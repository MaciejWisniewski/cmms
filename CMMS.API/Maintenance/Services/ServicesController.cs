using CMMS.Application.Maintenance.Services.ScheduleService;
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
    }
}
