using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.GetFailureInProgressByWorkerId
{
    public class GetFailureInProgressByWorkerIdQueryHandler : IQueryHandler<GetFailureInProgressByWorkerIdQuery, FailureDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetFailureInProgressByWorkerIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<FailureDto> Handle(GetFailureInProgressByWorkerIdQuery request, CancellationToken cancellationToken)
        {
			var connection = _sqlConnectionFactory.GetOpenConnection();
			string sql = @$"SELECT F.[Id] AS [{nameof(FailureDto.Id)}],
	                            F.[ResourceId] AS [{nameof(FailureDto.ResourceId)}] ,
							    R.[Name] AS [{nameof(FailureDto.ResourceName)}],
	                            F.[WorkerId] AS [{nameof(FailureDto.WorkerId)}],
							    W.[UserName] AS [{nameof(FailureDto.WorkerUserName)}],
	                            F.[State] AS [{nameof(FailureDto.State)}],
							    F.[ProblemDescription] AS [{nameof(FailureDto.ProblemDescription)}],		
	                            F.[Note] AS [{nameof(FailureDto.Note)}],
							    F.[OccurredOn] AS [{nameof(FailureDto.OccurredOn)}],
	                            F.[ResolvedOn] AS [{nameof(FailureDto.ResolvedOn)}]
                         FROM [maintenance].[Failures] AS F 
                            INNER JOIN [maintenance].[Resources] AS R 
                                ON F.[ResourceId] = R.[Id] 
                            LEFT JOIN [maintenance].[Workers] AS W
                                ON F.[WorkerId] = W.[Id] 
                         WHERE F.[WorkerId] = @WorkerId
                         AND F.[State] = 'InProgress' 
                         AND F.[ResolvedOn] IS NULL";
            var failures = await connection.QueryAsync<FailureDto>(sql, new { request.WorkerId });
            return failures.FirstOrDefault();
        }
    }
}
