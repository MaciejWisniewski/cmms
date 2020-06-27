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
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, JwtTokenDto>
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public AuthenticateCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<JwtTokenDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var username = request.Username;
            var password = request.Password;

            AppUser user = await _userManager.FindByNameAsync(username);
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) && user != null)
            {

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokens.Key));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, username)
                    };

                    var token = new JwtSecurityToken(
                        JwtTokens.Issuer,
                        JwtTokens.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: credentials);

                    return new JwtTokenDto()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        ExpirationDate = token.ValidTo
                    };
                }
            }

            return null;
        }
    }
}
