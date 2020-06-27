using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CMMS.Application.Identity.Authenticate;
using MediatR;

namespace CMMS.API.Identity.Authenticate
{
    [Route("api/[controller]")]
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
        [ProducesResponseType(typeof(JwtTokenDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentialsDto credentialsDto)
        {
            var jwtToken = await _mediator.Send(
                new AuthenticateCommand(
                    credentialsDto.Username, 
                    credentialsDto.Password
                    ));

            if(jwtToken == null)
                return BadRequest("Given username or password is incorrect");


            return Ok(jwtToken);
        }
    }
}