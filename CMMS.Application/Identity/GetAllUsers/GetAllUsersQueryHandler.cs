using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.GetAllUsers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[User].[Id] AS [{nameof(UserDto.Id)}], " +
                         $"[User].[FullName] AS [{nameof(UserDto.FullName)}], " +
                         $"[User].[UserName] AS [{nameof(UserDto.UserName)}], " +
                         $"[User].[Email] AS [{nameof(UserDto.Email)}], " +
                         $"[User].[PhoneNumber] AS [{nameof(UserDto.PhoneNumber)}], " +
                         $"[User].[IsActive] AS [{nameof(UserDto.IsActive)}], " +
                         $"[Role].[Name] AS [{nameof(UserDto.Role)}] " +
                         "FROM [CMMS].[dbo].[AspNetUsers] AS [User] " +
                         "LEFT JOIN [CMMS].[dbo].[AspNetUserRoles] AS [UserRole] " +
                         "ON [UserRole].[UserId] = [User].[Id] " +
                         "LEFT JOIN [CMMS].[dbo].[AspNetRoles] AS [Role] " +
                         "ON [UserRole].[RoleId] = [Role].[Id] " +
                         "WHERE [User].[IsActive] = 1";
            var users = await connection.QueryAsync<UserDto>(sql);

            return users.AsList();
        }
    }
}
