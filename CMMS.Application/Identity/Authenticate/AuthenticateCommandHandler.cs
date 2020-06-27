using CMMS.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticationResult>
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public AuthenticateCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var userName = request.UserName;
            var password = request.Password;

            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokens.Key));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, userName)
                    };

                    var token = new JwtSecurityToken(
                        JwtTokens.Issuer,
                        JwtTokens.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
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
