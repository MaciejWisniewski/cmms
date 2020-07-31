using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CMMS.Application.Maintenance.Workers;
using CMMS.Application.Maintenance.Workers.GetAllWorkres;
using CMMS.Application.Maintenance.Workers.GetWorkersHavingAccessTo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Maintenance.Workers
{
    [Route("api/maintenance/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all workers
        /// </summary>
        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(typeof(List<WorkerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllWorkers()
        {
            var workers = await _mediator.Send(new GetAllWorkersQuery());

            return Ok(workers);
        }

        /// <summary>
        /// Get workers having access to the resource with the given id.
        /// </summary>
        [HttpGet("all/{resourceId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<WorkerDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetWorkersHavingAccessTo([FromRoute]Guid resourceId)
        {
            var workers = await _mediator.Send(new GetWorkersHavingAccessToQuery(resourceId));

            return Ok(workers);
        }
    }
}
