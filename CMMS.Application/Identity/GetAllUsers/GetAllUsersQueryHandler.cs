using CMMS.Application.Configuration.Data;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[User].FullName, " +
                               "[User].UserName, " +
                               "[User].Email, " +
                               "[User].PhoneNumber," +
                               "[Role].Name " +
                               "FROM [CMMS].[dbo].[AspNetUsers] AS [User] " +
                               "LEFT JOIN [CMMS].[dbo].[AspNetUserRoles] AS [UserRole] " +
                               "ON [UserRole].UserId = [User].Id " +
                               "LEFT JOIN [CMMS].[dbo].[AspNetRoles] AS [Role] " +
                               "ON [UserRole].RoleId = [Role].Id";
            var users = await connection.QueryAsync<UserDto>(sql);

            return users.AsList();
        }
    }
}
