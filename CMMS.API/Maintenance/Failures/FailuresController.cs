using CMMS.API.Configuration;
using CMMS.Application.Maintenance.Failures;
using CMMS.Application.Maintenance.Failures.ChangeFailureState;
using CMMS.Application.Maintenance.Failures.FinishFailureRepair;
using CMMS.Application.Maintenance.Failures.GetAllFailuresInTimeRange;
using CMMS.Application.Maintenance.Failures.GetFailureInProgressByWorkerId;
using CMMS.Application.Maintenance.Failures.GetFailuresInTimeRangeByResourceId;
using CMMS.Application.Maintenance.Failures.RegisterFailure;
using CMMS.Application.Maintenance.Failures.StartFailureRepair;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authentication;
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
        /// Get all failures which occurred in the given time range
        /// </summary>
        [HttpGet("all/{from}/{to}")]
        [Authorize(Roles = UserRole.Leader)]
        [ProducesResponseType(typeof(List<GetAllFailuresInTimeRangeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> GetAllFailuresInTimeRange(DateTime from, DateTime to)
        {
            var failures = await _mediator.Send(new GetAllFailuresInTimeRangeQuery(from, to));

            return Ok(failures);
        }

        /// <summary>
        /// Get all failures registered for a resource with the given id that occurred in the given time range
        /// </summary>
        [HttpGet("all/{resourceId}/{from}/{to}")]
        [Authorize(Roles =UserRole.Leader)]
        [ProducesResponseType(typeof(List<GetFailuresInTimeRangeByResourceIdDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> GetFailuresInTimeRangeByResourceId([FromRoute] Guid resourceId, DateTime from, DateTime to)
        {
            var failures = await _mediator.Send(new GetFailuresInTimeRangeByResourceIdQuery(
                resourceId, 
                from,
                to));

            return Ok(failures);
        }

        /// <summary>
        /// Get all failures registered for resources that given worker has access to.
        /// </summary>
        [HttpGet("all/{workerId}")]
        [Authorize]
        [ProducesResponseType(typeof(List<FailureDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFailuresWorkerHasAccessTo([FromRoute] Guid workerId)
        {
            var failures = await _mediator.Send(new GetFailureInProgressByWorkerIdQuery(workerId));

            return Ok(failures);
        }



        /// <summary>
        /// Get failure in progress 
        /// </summary>
        [HttpGet("inProgress")]
        [Authorize]
        [ProducesResponseType(typeof(FailureDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetFailureInProgress()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var failure = await _mediator.Send(new GetFailureInProgressByWorkerIdQuery(JwtTokenHelper.ExtractUserId(accessToken)));

            return Ok(failure);
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

        [Obsolete]
        [HttpPut("{failureId}")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> ChangeFailureState([FromRoute] Guid failureId, [FromBody] ChangeFailureStateRequest changeFailureStateRequest)
        {
            await _mediator.Send(new ChangeFailureStateCommand(
                failureId,
                changeFailureStateRequest.WorkerId,
                changeFailureStateRequest.Note,
                changeFailureStateRequest.FailureState));

            return Ok();
        }

        /// <summary>
        /// Start to repair a failure with the given id
        /// </summary>
        [HttpPatch("{failureId}/start")]
        [Authorize(Roles = UserRole.User)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> StartFailureRepair([FromRoute] Guid failureId)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _mediator.Send(new StartFailureRepairCommand(failureId, JwtTokenHelper.ExtractUserId(accessToken)));

            return Ok();
        }

        /// <summary>
        /// Finish repairing a failure with the given id
        /// </summary>
        [HttpPatch("{failureId}/finish")]
        [Authorize(Roles = UserRole.User)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> FinishFailureRepair([FromRoute] Guid failureId, [FromBody] FinishFailureRepairRequest request)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            await _mediator.Send(new FinishFailureRepairCommand(
                failureId,
                JwtTokenHelper.ExtractUserId(accessToken),
                request.Note));

            return Ok();
        }


    }
}
