using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.GetAllResources
{
    public class GetAllResourcesQueryHandler : IQueryHandler<GetAllResourcesQuery, List<ResourceDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllResourcesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ResourceDto>> Handle(GetAllResourcesQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Resource].[Id] AS [{nameof(ResourceDto.Id)}], " +
                         $"[Resource].[ParentId] AS [{nameof(ResourceDto.ParentId)}], " +
                         $"[Resource].[Name] AS [{nameof(ResourceDto.Name)}], " +
                         $"[Resource].[IsArea] AS [{nameof(ResourceDto.IsArea)}], " +
                         $"[Resource].[IsMachine] AS [{nameof(ResourceDto.IsMachine)}] " +
                         "FROM [CMMS].[maintenance].[Resources] AS [Resource]";
            var resources = await connection.QueryAsync<ResourceDto>(sql);

            return resources.AsList();
        }
    }
}
