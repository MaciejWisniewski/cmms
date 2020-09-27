using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.GetServiceInProgressByWorkerId
{
    public class GetServiceInProgressByWorkerIdQueryHandler : IQueryHandler<GetServiceInProgressByWorkerIdQuery, GetServiceInProgressByWorkerIdDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetServiceInProgressByWorkerIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<GetServiceInProgressByWorkerIdDto> Handle(GetServiceInProgressByWorkerIdQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
			string sql = @$"SELECT S.[Id] AS [{nameof(GetServiceInProgressByWorkerIdDto.Id)}],
	                        S.[ResourceId] AS [{nameof(GetServiceInProgressByWorkerIdDto.ResouceId)}] ,
							R.[Name] AS [{nameof(GetServiceInProgressByWorkerIdDto.ResourceName)}],
	                        S.[TypeId] AS [{nameof(GetServiceInProgressByWorkerIdDto.TypeId)}],
							ST.[Name] AS [{nameof(GetServiceInProgressByWorkerIdDto.TypeName)}],
	                        S.[ScheduledWorkerId] AS [{nameof(GetServiceInProgressByWorkerIdDto.ScheduledWorkerId)}],
							WS.[FullName] AS [{nameof(GetServiceInProgressByWorkerIdDto.ScheduledWorkerName)}],		
	                        S.[ActualWorkerId] AS [{nameof(GetServiceInProgressByWorkerIdDto.ActualWorkerId)}],
							WA.[FullName] AS [{nameof(GetServiceInProgressByWorkerIdDto.ActualWorkerName)}],
	                        S.[ScheduledStartDateTime] AS [{nameof(GetServiceInProgressByWorkerIdDto.ScheduledStartDateTime)}],
	                        S.[ScheduledEndDateTime] AS [{nameof(GetServiceInProgressByWorkerIdDto.ScheduledEndDateTime)}],
	                        S.[ActualStartDateTime] AS [{nameof(GetServiceInProgressByWorkerIdDto.ActualStartDateTime)}],
	                        S.[ActualEndDateTime] AS [{nameof(GetServiceInProgressByWorkerIdDto.ActualEndDateTime)}],
	                        S.[Description] AS [{nameof(GetServiceInProgressByWorkerIdDto.Description)}],
							S.[Note] AS [{nameof(GetServiceInProgressByWorkerIdDto.Note)}]
                        FROM [maintenance].[Services] AS S
                        INNER JOIN [maintenance].[Resources] AS R
						ON S.[ResourceId] = R.[Id]
						INNER JOIN [maintenance].[ServiceTypes] AS ST
						ON S.[TypeId] = ST.[Id]
						LEFT JOIN [maintenance].[Workers] AS WS
						ON S.[ScheduledWorkerId] = WS.[Id]
						LEFT JOIN [maintenance].[Workers] AS WA
						ON S.[ActualWorkerId] = WA.[Id]
                        WHERE S.[ActualWorkerId] = @workerId
						AND S.[ActualEndDateTime] IS NULL";
			return await connection.QueryFirstOrDefaultAsync<GetServiceInProgressByWorkerIdDto>(sql, new { workerId = request.WorkerId });
		
		}
    }
}
