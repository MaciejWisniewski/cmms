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
        private readonly IAppRoleService _appRoleService;

        public AppUsersController(IAppUserService appUserService, IAppRoleService appRoleService)
        {
            _appUserService = appUserService;
            _appRoleService = appRoleService;
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
            var createResult = await _appUserService.CreateAsync(userDto);

            if (!createResult.Succeeded)
                return BadRequest(createResult.Errors);

            if (!string.IsNullOrWhiteSpace(userDto.Role) && await _appRoleService.RoleExistsAsync(userDto.Role))
            {
                var addToRoleResult = await _appUserService.AddToRoleAsync(userDto);

                if (!addToRoleResult.Succeeded)
                    return BadRequest(addToRoleResult.Errors);
            }

            var createdUserDto = await _appUserService.GetByUserNameAsync(userDto.UserName);

            return Ok(createdUserDto);
        }
    }
}