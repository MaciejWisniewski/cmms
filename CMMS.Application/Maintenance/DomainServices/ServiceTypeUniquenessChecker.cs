using CMMS.Application.Configuration.Data;
using CMMS.Domain.Maintenance.ServiceTypes;
using Dapper;

namespace CMMS.Application.Maintenance.DomainServices
{
    public class ServiceTypeUniquenessChecker : IServiceTypeUniquenessChecker
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ServiceTypeUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public bool IsUnique(string typeName)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT TOP 1 1" +
                               " FROM [CMMS].[maintenance].[ServiceTypes] AS [ServiceType] " +
                               "WHERE [ServiceType].[Name] = @Name";
            var serviceTypesCount = connection.QuerySingleOrDefault<int?>(sql,
                            new
                            {
                                Name = typeName
                            });

            return !serviceTypesCount.HasValue;
        }
    }
}
