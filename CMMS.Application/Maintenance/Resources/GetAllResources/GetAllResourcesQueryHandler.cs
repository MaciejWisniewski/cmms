using CMMS.Application.Configuration.Data;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Resources.GetAllResources
{
    public class GetAllResourcesQueryHandler : IRequestHandler<GetAllResourcesQuery, List<ResourceDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllResourcesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ResourceDto>> Handle(GetAllResourcesQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                                "[Resource].[Id], " +
                                "[Resource].[ParentId], " +
                                "[Resource].[Name], " +
                                "[Resource].[IsArea], " +
                                "[Resource].[IsMachine] " +
                                "FROM [CMMS].[maintenance].[Resources] AS [Resource]";

            var resources = await connection.QueryAsync<ResourceDto>(sql);

            return resources.AsList();
        }
    }
}
