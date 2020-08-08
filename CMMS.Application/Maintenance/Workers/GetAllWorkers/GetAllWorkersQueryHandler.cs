using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using CMMS.Application.Maintenance.Workers.GetlAllWorkers;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Workers.GetAllWorkers
{
    public class GetAllWorkersQueryHandler : IQueryHandler<GetAllWorkersQuery, List<WorkerDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllWorkersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<WorkerDto>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
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
                         "ORDER BY [Worker].[FullName] ASC";
            var resources = await connection.QueryAsync<WorkerDto>(sql);

            return resources.AsList();
        }
    }
}
