﻿using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Validation;
using Dapper;
using MediatR;
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
                               "[User].Id, " +
                               "[User].FullName, " +
                               "[User].UserName, " +
                               "[User].Email, " +
                               "[User].PhoneNumber," +
                               "[Role].Name AS Role " +
                               "FROM [CMMS].[dbo].[AspNetUsers] AS [User] " +
                               "LEFT JOIN [CMMS].[dbo].[AspNetUserRoles] AS [UserRole] " +
                               "ON [UserRole].UserId = [User].Id " +
                               "LEFT JOIN [CMMS].[dbo].[AspNetRoles] AS [Role] " +
                               "ON [UserRole].RoleId = [Role].Id " +
                               "WHERE [User].Id = @Id";
            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { query.Id});

            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            return user;

        }
    }
}
