using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.GetFailuresInTimeRangeByResourceId
{
    public class GetFailuresInTimeRangeByResourceIdQueryHandler : IQueryHandler<GetFailuresInTimeRangeByResourceIdQuery, List<GetFailuresInTimeRangeByResourceIdDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetFailuresInTimeRangeByResourceIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<GetFailuresInTimeRangeByResourceIdDto>> Handle(
            GetFailuresInTimeRangeByResourceIdQuery query, 
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Failure].[Id] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.Id)}], " +
                         $"[Failure].[WorkerId] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.WorkerId)}], " +
                         $"[Worker].[UserName] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.WorkerUserName)}], " +
                         $"[Failure].[State] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.State)}], " +
                         $"[Failure].[ProblemDescription] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.ProblemDescription)}], " +
                         $"[Failure].[Note] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.Note)}], " +
                         $"[Failure].[OccurredOn] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.OccurredOn)}], " +
                         $"[Failure].[ResolvedOn] AS [{nameof(GetFailuresInTimeRangeByResourceIdDto.ResolvedOn)}] " +
                         "FROM [CMMS].[maintenance].[Failures] AS [Failure] " +
                         "LEFT JOIN [CMMS].[maintenance].[Workers] AS [Worker] " +
                         "ON [Failure].[WorkerId] = [Worker].[Id] " +
                         "WHERE [Failure].[ResourceId] = @ResourceId " +
                         "AND [Failure].[OccurredOn] BETWEEN @From AND @To " +
                         "ORDER BY [Failure].[OccurredOn] ASC";
            var failures = await connection.QueryAsync<GetFailuresInTimeRangeByResourceIdDto>(sql, 
                new
                {
                    ResourceId = query.ResourceId,
                    From = query.From,
                    To = query.To
                });

            return failures.AsList();
        }
    }
}
