using System.Threading.Tasks;
using CMMS.DTOs;
using CMMS.Models;
using CMMS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = UserRole.Admin)]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AppUsersController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var appUsers = await _appUserService.GetAllAsync();

            return Ok(appUsers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AppUserDto userDto)
        {
            var result = await _appUserService.CreateAsync(userDto);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var createdUserDto = await _appUserService.GetByUserNameAsync(userDto.UserName);

            return Ok(createdUserDto);
        }
    }
}