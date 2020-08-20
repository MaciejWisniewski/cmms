using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.GetAllFailuresInTimeRange
{
    public class GetAllFailuresInTimeRangeQueryHandler :
        IQueryHandler<GetAllFailuresInTimeRangeQuery, List<GetAllFailuresInTimeRangeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllFailuresInTimeRangeQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<GetAllFailuresInTimeRangeDto>> Handle(GetAllFailuresInTimeRangeQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Failure].[Id] AS [{nameof(GetAllFailuresInTimeRangeDto.Id)}], " +
                         $"[Failure].[ResourceId] AS [{nameof(GetAllFailuresInTimeRangeDto.ResourceId)}], " +
                         $"[Resource].[Name] AS [{nameof(GetAllFailuresInTimeRangeDto.ResourceName)}], " +
                         $"[Failure].[State] AS [{nameof(GetAllFailuresInTimeRangeDto.State)}], " +
                         $"[Failure].[OccurredOn] AS [{nameof(GetAllFailuresInTimeRangeDto.OccurredOn)}], " +
                         $"[Failure].[ResolvedOn] AS [{nameof(GetAllFailuresInTimeRangeDto.ResolvedOn)}] " +
                         "FROM [CMMS].[maintenance].[Failures] AS [Failure] " +
                         "JOIN [CMMS].[maintenance].[Resources] AS [Resource] " +
                         "ON [Failure].[ResourceId] = [Resource].[Id] " +
                         "WHERE [Failure].[OccurredOn] BETWEEN @From AND @To " +
                         "ORDER BY [Resource].[Name] ASC";
            var failures = await connection.QueryAsync<GetAllFailuresInTimeRangeDto>(sql, new
            {
                From = query.From,
                To = query.To
            });

            return failures.AsList();
        }
    }
}
