using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CMMS.Application.Identity.Authenticate;
using MediatR;

namespace CMMS.API.Identity.Authenticate
{
    [Route("api/identity/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Authenticate user by given credentials.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticationResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentialsDto credentialsDto)
        {
            var result = await _mediator.Send(
                new AuthenticateCommand(
                    credentialsDto.UserName, 
                    credentialsDto.Password
                    ));

            return Ok(result);
        }
    }
}