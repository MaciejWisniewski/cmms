using CMMS.Application.Maintenance.Failures.RegisterFailure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
