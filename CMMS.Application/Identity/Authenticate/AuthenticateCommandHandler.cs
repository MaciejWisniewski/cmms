using CMMS.Application.Configuration.Commands;
using CMMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private SignInManager<AppUser> _signInManager { get; }

        public AuthenticateCommandHandler(IUserRepository userRepository, SignInManager<AppUser> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            var userName = command.UserName;
            var password = command.Password;

            AppUser user = await _userRepository.GetByUserNameAsync(userName);
            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokens.Key));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var roles = await _userRepository.GetRolesAsync(user);

                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userName)
                    };
                    claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

                    var token = new JwtSecurityToken(
                        JwtTokens.Issuer,
                        JwtTokens.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddDays(90),
                        signingCredentials: credentials);

                    return new AuthenticationResult(
                        token: new JwtSecurityTokenHandler().WriteToken(token), 
                        expirationDate: token.ValidTo);
                }
            }

            return new AuthenticationResult("Username or password is incorrect");
        }
    }
}
