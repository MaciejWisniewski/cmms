using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.GetServicesByWorkerAccesses
{
    public class GetServicesByWorkerAccessesQueryHandler : IQueryHandler<GetServicesByWorkerAccessesQuery, List<ServiceDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetServicesByWorkerAccessesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<ServiceDto>> Handle(GetServicesByWorkerAccessesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = @$"SELECT S.[Id] AS [{nameof(ServiceDto.Id)}],
	                        S.[ResourceId] AS [{nameof(ServiceDto.ResouceId)}] ,
							R.[Name] AS [{nameof(ServiceDto.ResourceName)}],
	                        S.[TypeId] AS [{nameof(ServiceDto.TypeId)}],
	                        S.[ScheduledWorkerId] AS [{nameof(ServiceDto.ScheduledWorkerId)}],
	                        S.[ActualWorkerId] AS [{nameof(ServiceDto.ActualWorkerId)}],
	                        S.[ScheduledStartDateTime] AS [{nameof(ServiceDto.ScheduledStartDateTime)}],
	                        S.[ScheduledEndDateTime] AS [{nameof(ServiceDto.ScheduledEndDateTime)}],
	                        S.[ActualStartDateTime] AS [{nameof(ServiceDto.ActualStartDateTime)}],
	                        S.[ActualEndDateTime] AS [{nameof(ServiceDto.ActualEndDateTime)}],
	                        S.[Description] AS [{nameof(ServiceDto.Description)}]
                        FROM [maintenance].[Services] AS S
                        INNER JOIN [maintenance].[Resources] AS R
						ON S.[ResourceId] = R.[Id]
                        WHERE S.[ResourceId] IN (
		                        SELECT RA.[ResourceId]
		                        FROM [maintenance].[ResourceAccesses] AS RA
		                        WHERE RA.[WorkerId] = @workerId)";
            var services = await connection.QueryAsync<ServiceDto>(sql, new { workerId = request.WorkerId });

            return services.AsList();
        }
    }
}
