using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMS.Models;
using CMMS.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}