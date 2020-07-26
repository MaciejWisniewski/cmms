using CMMS.Application.Configuration.Data;
using CMMS.Domain.Identity;
using Dapper;

namespace CMMS.Application.Identity.DomainServices
{
    public class RoleValidator : IRoleValidator
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public RoleValidator(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public string GetValidOrDefault(string roleName)
        {
            return IsValid(roleName) ? roleName : UserRole.Default;
        }

        private bool IsValid(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return false;

            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1 " +
                               "FROM [CMMS].[dbo].[AspNetRoles] as [Role] " +
                               "WHERE [Role].NormalizedName = @NormalizedName";
            var rolesCount = connection.QuerySingleOrDefault<int?>(sql, new { NormalizedName = roleName.Normalize().ToUpperInvariant() });

            return rolesCount.HasValue;
        }
    }
}
