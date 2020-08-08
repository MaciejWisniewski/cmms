using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Workers.GetWorkersHavingAccessTo
{
    public class GetWorkersHavingAccessToQueryHandler : IQueryHandler<GetWorkersHavingAccessToQuery, List<WorkerDto>>
    {        
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetWorkersHavingAccessToQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<WorkerDto>> Handle(GetWorkersHavingAccessToQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Worker].[Id] AS [{nameof(WorkerDto.Id)}], " +
                         $"[Worker].[UserName] AS [{nameof(WorkerDto.UserName)}], " +
                         $"[Worker].[Email] AS [{nameof(WorkerDto.Email)}], " +
                         $"[Worker].[FullName] AS [{nameof(WorkerDto.FullName)}], " +
                         $"[Worker].[PhoneNumber] AS [{nameof(WorkerDto.PhoneNumber)}], " +
                         $"[Worker].[Role] AS [{nameof(WorkerDto.Role)}] " +
                         "FROM [CMMS].[maintenance].[Workers] AS [Worker] " +
                         "JOIN [CMMS].[maintenance].[ResourceAccesses] AS [Access] " +
                         "ON [Worker].[Id] = [Access].[WorkerId] " +
                         "WHERE [Access].[ResourceId] = @ResourceId " +
                         "ORDER BY [Worker].[FullName] ASC";

            var resources = await connection.QueryAsync<WorkerDto>(sql, new { query.ResourceId });

            return resources.AsList();
        }
    }
}
