using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace CMMS.API.Configuration
{
    public class JwtTokenHelper
    {
        public static Guid ExtractUserId(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            var id = token.Claims.First(c => c.Type == "id").Value;

            return new Guid(id);
        }
    }
}
