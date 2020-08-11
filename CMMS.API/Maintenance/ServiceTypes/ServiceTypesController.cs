using CMMS.Application.Maintenance.ServiceTypes.AddServiceType;
using CMMS.Application.Maintenance.ServiceTypes.GetAllServiceTypes;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        /// Get all service types.
        /// </summary>
        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(typeof(List<ServiceTypeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllServiceTypes()
        {
            var serviceTypes = await _mediator.Send(new GetAllServiceTypesQuery());

            return Ok(serviceTypes);
        }

        /// <summary>
        /// Add new service type.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = UserRole.Leader)]
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
