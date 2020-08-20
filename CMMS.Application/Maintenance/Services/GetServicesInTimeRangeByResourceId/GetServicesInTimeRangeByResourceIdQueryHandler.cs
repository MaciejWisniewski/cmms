using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.GetServicesInTimeRangeByResourceId
{
    public class GetServicesInTimeRangeByResourceIdQueryHandler : 
        IQueryHandler<GetServicesInTimeRangeByResourceIdQuery, List<GetServicesInTimeRangeByResourceIdDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetServicesInTimeRangeByResourceIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<GetServicesInTimeRangeByResourceIdDto>> Handle(
            GetServicesInTimeRangeByResourceIdQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Service].[Id] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.Id)}], " +
                         $"[Service].[TypeId] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.TypeId)}], " +
                         $"[ServiceType].[Name] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.TypeName)}], " +
                         $"[Service].[ScheduledWorkerId] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ScheduledWorkerId)}], " +
                         $"[ScheduledWorker].[UserName] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ScheduledWorkerUserName)}], " +
                         $"[Service].[ActualWorkerId] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ActualWorkerId)}], " +
                         $"[ActualWorker].[UserName] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ActualWorkerUserName)}], " +
                         $"[Service].[Description] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.Description)}], " +
                         $"[Service].[Note] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.Note)}], " +
                         $"[Service].[ScheduledStartDateTime] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ScheduledStartDateTime)}], " +
                         $"[Service].[ScheduledEndDateTime] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ScheduledEndDateTime)}], " +
                         $"[Service].[ActualStartDateTime] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ActualStartDateTime)}], " +
                         $"[Service].[ActualEndDateTime] AS [{nameof(GetServicesInTimeRangeByResourceIdDto.ActualEndDateTime)}] " +
                         "FROM [CMMS].[maintenance].[Services] AS [Service] " +
                         "LEFT JOIN [CMMS].[maintenance].[ServiceTypes] AS [ServiceType] " +
                         "ON [Service].[TypeId] = [ServiceType].[Id] " +
                         "LEFT JOIN [CMMS].[maintenance].[Workers] AS [ScheduledWorker] " +
                         "ON [Service].[ScheduledWorkerId] = [ScheduledWorker].[Id] " +
                         "LEFT JOIN [CMMS].[maintenance].[Workers] AS [ActualWorker] " +
                         "ON [Service].[ActualWorkerId] = [ActualWorker].[Id] " +
                         "WHERE ResourceId = @ResourceId " +
                         "AND [Service].[ScheduledStartDateTime] BETWEEN @From AND @To " +
                         "ORDER BY [Service].[ScheduledStartDateTime] ASC";
            var services = await connection.QueryAsync<GetServicesInTimeRangeByResourceIdDto>(sql,
                new
                {
                    ResourceId = query.ResourceId,
                    From = query.From,
                    To = query.To
                });

            return services.AsList();
        }
    }
}
