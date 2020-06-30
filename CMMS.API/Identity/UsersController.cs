using CMMS.Application.Identity.CreateUser;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace CMMS.API.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            var user = await _mediator.Send(new CreateUserCommand(
                    request.FullName,
                    request.UserName,
                    request.Email,
                    request.PhoneNumber,
                    request.Password,
                    request.Role
                ));

            return Ok(user);
        }
    }
}