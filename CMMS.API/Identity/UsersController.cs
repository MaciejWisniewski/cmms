using CMMS.Application.Identity;
using CMMS.Application.Identity.ChangeUserRole;
using CMMS.Application.Identity.CreateUser;
using CMMS.Application.Identity.GetAllUsers;
using CMMS.Application.Identity.GetUser;
using CMMS.Application.Identity.RemoveUser;
using CMMS.Application.Identity.UpdateUser;
using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CMMS.API.Identity
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetByUserName([FromRoute]Guid id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));

            return Ok(user);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        [Authorize]
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(List<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            return Ok(users);
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            var userId = await _mediator.Send(new CreateUserCommand(
                    request.FullName,
                    request.UserName,
                    request.Email,
                    request.PhoneNumber,
                    request.Password,
                    request.Role
                ));

            return Ok(userId);
        }

        /// <summary>
        /// Update user's data.
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody]UpdateUserRequest request)
        {
            await _mediator.Send(new UpdateUserCommand(
                    id,
                    request.FullName,
                    request.Email,
                    request.PhoneNumber
                ));

            return Ok();
        }

        /// <summary>
        /// Change user's role.
        /// </summary>
        [HttpPut]
        [Route("{userId}/roles/changeUserRole")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ChangeUserRole([FromRoute] Guid userId, [FromBody] ChangeUserRoleRequest request)
        {
            await _mediator.Send(new ChangeUserRoleCommand(userId, request.RoleId));

            return Ok();
        }

        /// <summary>
        /// Remove user with the given id.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRole.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> RemoveUser([FromRoute]Guid id)
        {
            await _mediator.Send(new RemoveUserCommand(id));

            return Ok();
        }
    }
}