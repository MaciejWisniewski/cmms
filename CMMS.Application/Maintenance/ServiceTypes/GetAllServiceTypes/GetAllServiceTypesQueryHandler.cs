using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.ServiceTypes.GetAllServiceTypes
{
    public class GetAllServiceTypesQueryHandler : IQueryHandler<GetAllServiceTypesQuery, List<ServiceTypeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllServiceTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ServiceTypeDto>> Handle(GetAllServiceTypesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[ServiceType].[Id] AS [{nameof(ServiceTypeDto.Id)}], " +
                         $"[ServiceType].[Name] AS [{nameof(ServiceTypeDto.Name)}] " +
                         "FROM [CMMS].[maintenance].[ServiceTypes] AS [ServiceType]" +
                         "ORDER BY [ServiceType].[Name]";
            var serviceTypes = await connection.QueryAsync<ServiceTypeDto>(sql);

            return serviceTypes.AsList();
        }
    }
}
