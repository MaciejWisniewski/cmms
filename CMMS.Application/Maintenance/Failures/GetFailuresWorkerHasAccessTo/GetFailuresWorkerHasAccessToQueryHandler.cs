using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.GetFailuresWorkerHasAccessTo
{
    public class GetFailuresWorkerHasAccessToQueryHandler : IQueryHandler<GetFailuresWorkerHasAccessToQuery, List<FailureDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetFailuresWorkerHasAccessToQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<FailureDto>> Handle(GetFailuresWorkerHasAccessToQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Failure].[Id] AS [{nameof(FailureDto.Id)}], " +
                         $"[Failure].[ResourceId] AS [{nameof(FailureDto.ResourceId)}], " +
                         $"[Failure].[WorkerId] AS [{nameof(FailureDto.WorkerId)}], " +
                         $"[Failure].[State] AS [{nameof(FailureDto.State)}], " +
                         $"[Failure].[ProblemDescription] AS [{nameof(FailureDto.ProblemDescription)}], " +
                         $"[Failure].[Note] AS [{nameof(FailureDto.Note)}], " +
                         $"[Failure].[OccurredOn] AS [{nameof(FailureDto.OccurredOn)}], " +
                         $"[Failure].[ResolvedOn] AS [{nameof(FailureDto.ResolvedOn)}] " +
                         "FROM [CMMS].[maintenance].[Failures] AS [Failure] " +
                         "JOIN [CMMS].[maintenance].[ResourceAccesses] AS [Access] " +
                         "ON [Failure].[ResourceId] = [Access].[ResourceId] " +
                         "WHERE [Access].[WorkerId] = @WorkerId";
            var failures = await connection.QueryAsync<FailureDto>(sql, new { query.WorkerId });

            return failures.AsList();
        }
    }
}
