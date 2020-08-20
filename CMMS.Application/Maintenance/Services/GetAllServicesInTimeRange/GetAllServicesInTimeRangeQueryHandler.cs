using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Services.GetAllServicesInTimeRange
{
    public class GetAllServicesInTimeRangeQueryHandler : 
        IQueryHandler<GetAllServicesInTimeRangeQuery, List<GetAllServicesInTimeRangeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllServicesInTimeRangeQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<GetAllServicesInTimeRangeDto>> Handle(GetAllServicesInTimeRangeQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Service].[Id] AS [{nameof(GetAllServicesInTimeRangeDto.Id)}], " +
                         $"[Service].[ResourceId] AS [{nameof(GetAllServicesInTimeRangeDto.ResourceId)}], " +
                         $"[Resource].[Name] AS [{nameof(GetAllServicesInTimeRangeDto.ResourceName)}], " +
                         $"[Service].[TypeId] AS [{nameof(GetAllServicesInTimeRangeDto.TypeId)}], " +
                         $"[ServiceType].[Name] AS [{nameof(GetAllServicesInTimeRangeDto.TypeName)}], " +
                         $"[Service].[ScheduledStartDateTime] AS [{nameof(GetAllServicesInTimeRangeDto.ScheduledStartDateTime)}], " +
                         $"[Service].[ScheduledEndDateTime] AS [{nameof(GetAllServicesInTimeRangeDto.ScheduledEndDateTime)}], " +
                         $"[Service].[ActualStartDateTime] AS [{nameof(GetAllServicesInTimeRangeDto.ActualStartDateTime)}], " +
                         $"[Service].[ActualEndDateTime] AS [{nameof(GetAllServicesInTimeRangeDto.ActualEndDateTime)}] " +
                         "FROM [CMMS].[maintenance].[Services] AS [Service] " +
                         "LEFT JOIN [CMMS].[maintenance].[Resources] AS [Resource] " +
                         "ON [Resource].[Id] = [Service].[ResourceId] " +
                         "LEFT JOIN [CMMS].[maintenance].[ServiceTypes] AS [ServiceType] " +
                         "ON [Service].[TypeId] = [ServiceType].[Id] " +
                         "WHERE [Service].[ScheduledStartDateTime] BETWEEN @From AND @To " +
                         "ORDER BY [Resource].[Name] ASC";
            var services = await connection.QueryAsync<GetAllServicesInTimeRangeDto>(sql, new
            {
                @From = query.From,
                @To = query.To
            });

            return services.AsList();
        }
    }
}
