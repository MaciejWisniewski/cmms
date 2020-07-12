using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CMMS.Application.Identity;
using CMMS.Application.Identity.GetAllRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.API.Identity
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all roles.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<RoleDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());

            return Ok(roles);
        }
    }
}