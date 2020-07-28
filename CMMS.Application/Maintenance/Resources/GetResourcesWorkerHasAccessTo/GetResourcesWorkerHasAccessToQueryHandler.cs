using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using CMMS.Application.Maintenance.Resources.GetAllResources;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.GetResourcesWorkerHasAccessTo
{
    public class GetResourcesWorkerHasAccessToQueryHandler : IQueryHandler<GetResourcesWorkerHasAccessToQuery, List<ResourceDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetResourcesWorkerHasAccessToQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ResourceDto>> Handle(GetResourcesWorkerHasAccessToQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Resource].[Id] AS [{nameof(ResourceDto.Id)}], " +
                         $"[Resource].[ParentId] AS [{nameof(ResourceDto.ParentId)}], " +
                         $"[Resource].[Name] AS [{nameof(ResourceDto.Name)}], " +
                         $"[Resource].[IsArea] AS [{nameof(ResourceDto.IsArea)}], " +
                         $"[Resource].[IsMachine] AS [{nameof(ResourceDto.IsMachine)}] " +
                         "FROM [CMMS].[maintenance].[Resources] AS [Resource] " +
                         "JOIN [CMMS].[maintenance].[ResourceAccesses] AS [Access] " +
                         "ON [Resource].[Id] = [Access].[ResourceId] " +
                         "WHERE [Access].[WorkerId] = @WorkerId";

            var resources = await connection.QueryAsync<ResourceDto>(sql, new { query.WorkerId });

            return resources.AsList();
        }
    }
}
