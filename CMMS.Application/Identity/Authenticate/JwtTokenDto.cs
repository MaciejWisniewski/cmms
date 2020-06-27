using System;

namespace CMMS.Application.Identity.Authenticate
{
    public class JwtTokenDto
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
