using System;

namespace CMMS.Application.Identity.Authenticate
{
    public class AuthenticationResult
    {
        public AuthenticationResult(string authenticationError)
        {
            IsAuthenticated = false;
            AuthenticationError = authenticationError;
        }

        public AuthenticationResult(string token, DateTime expirationDate)
        {
            IsAuthenticated = true;
            Token = token;
            ExpirationDate = expirationDate;
        }

        public bool IsAuthenticated { get; }

        public string AuthenticationError { get; }

        public string Token { get; }

        public DateTime ExpirationDate { get; }
    }
}
