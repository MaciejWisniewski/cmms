using CMMS.Application.Maintenance.ServiceTypes.AddServiceType;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CMMS.API.Maintenance.ServiceTypes
{
    [Route("api/maintenance/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add new service type.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddServiceType([FromBody]AddServiceTypeRequest request)
        {
            var serviceTypeId = await _mediator.Send(new AddServiceTypeCommand(request.Name));

            return Ok(serviceTypeId);
        }
    }
}
