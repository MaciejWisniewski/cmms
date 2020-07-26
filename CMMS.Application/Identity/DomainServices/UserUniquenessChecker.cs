using CMMS.Application.Configuration.Data;
using CMMS.Domain.Identity;
using Dapper;

namespace CMMS.Application.Identity.DomainServices
{
    public class UserUniquenessChecker : IUserUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public UserUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(string userName, string email)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1 " +
                               "FROM [CMMS].[dbo].[AspNetUsers] AS [User] " +
                               "WHERE [User].[NormalizedEmail] = @NormalizedEmail " + 
                               "OR [User].[NormalizedUserName] = @NormalizedUserName";

            var customersCount = connection.QuerySingleOrDefault<int?>(sql,
                            new
                            {
                                NormalizedUserName = userName.Normalize().ToUpperInvariant(),
                                NormalizedEmail = email.Normalize().ToUpperInvariant()
                            });

            return !customersCount.HasValue;
        }
    }
}
