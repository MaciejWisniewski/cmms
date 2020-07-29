using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.GetAllRoles
{
    public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllRolesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<RoleDto>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Role].[Id] AS [{nameof(RoleDto.Id)}], " +
                         $"[Role].[Name] AS [{nameof(RoleDto.Name)}] " +
                         "FROM [CMMS].[dbo].[AspNetRoles] AS [Role]";
            var roles = await connection.QueryAsync<RoleDto>(sql);

            return roles.AsList();
        }
    }
}
