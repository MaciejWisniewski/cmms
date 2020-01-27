using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CMMS.DTOs;
using CMMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CMMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserCredentialsDto credentialsDto)
        {
            var userName = credentialsDto.UserName;
            var password = credentialsDto.Password;

            var user = await _userManager.FindByNameAsync(userName);
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password) && user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MvsJwtTokens.Key));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var roles = await _userManager.GetRolesAsync(user);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Token");
                    claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                    var token = new JwtSecurityToken(
                        issuer: MvsJwtTokens.Issuer, 
                        audience: MvsJwtTokens.Audience, 
                        claims: claimsIdentity.Claims, 
                        expires: DateTime.UtcNow.AddDays(1), 
                        signingCredentials: credentials
                    );

                    var result = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expirationDate = token.ValidTo
                    };

                    return Ok(result);
                }
            }

            return BadRequest();
        }
    }
}