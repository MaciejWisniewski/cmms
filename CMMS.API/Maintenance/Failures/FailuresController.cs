using CMMS.Application.Maintenance.Failures;
using CMMS.Application.Maintenance.Failures.ChangeFailureState;
using CMMS.Application.Maintenance.Failures.GetFailuresWorkerHasAccessTo;
using CMMS.Application.Maintenance.Failures.RegisterFailure;
using CMMS.Domain.Maintenance.Failures;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CMMS.API.Maintenance.Failures
{
    [Route("api/maintenance/[controller]")]
    [ApiController]
    public class FailuresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FailuresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all failures registered for resources that given worker has access to.
        /// </summary>
        [HttpGet("all/{workerId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<FailureDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFailuresWorkerHasAccessTo([FromRoute]Guid workerId)
        {
            var resources = await _mediator.Send(new GetFailuresWorkerHasAccessToQuery(workerId));

            return Ok(resources);
        }


        /// <summary>
        /// Register detected failure.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RegisterFailure([FromBody] RegisterFailureRequest request)
        {
            var failureId = await _mediator.Send(new RegisterFailureCommand(
                request.ResourceId, 
                request.ProblemDescription));

            return Ok(failureId);
        }


        [HttpPut("{failureId}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ChangeFailureState([FromRoute]Guid failureId, [FromBody]ChangeFailureStateRequest changeFailureStateRequest)
        {
            await _mediator.Send(new ChangeFailureStateCommand(
                failureId,
                changeFailureStateRequest.WorkerId,
                changeFailureStateRequest.Note,
                changeFailureStateRequest.FailureState));
            return NoContent();
        }

    }
}
