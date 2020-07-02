using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Validation;
using Dapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
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
                               "ON [UserRole].RoleId = [Role].Id " +
                               "WHERE [User].UserName = @UserName";
            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { query.UserName });

            if (user == null)
                throw new NotFoundException("User with the given username hasn't been found", null);

            return user;

        }
    }
}
