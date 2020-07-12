using CMMS.Application.Configuration.Data;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Areas.GetAreas
{
    public class GetAllAreasQueryHandler : IRequestHandler<GetAllAreasQuery, List<AreaDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllAreasQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<AreaDto>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = 
                @"SELECT 
                    [Id], 
                    [Name]
                  FROM
                    [CMMS].[maintenance].[Areas]";
            var areas = await connection.QueryAsync<AreaDto>(sql);

            return areas.AsList();
        }
    }
}
