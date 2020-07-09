using CMMS.Application.Configuration.Data;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllRolesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[Role].Id, " +
                               "[Role].Name " +
                               "FROM [CMMS].[dbo].[AspNetRoles] AS [Role]";
            var roles = await connection.QueryAsync<RoleDto>(sql);

            return roles.AsList();
        }
    }
}
