using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Queries;
using CMMS.Application.Configuration.Validation;
using Dapper;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Identity.GetUser
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[User].[Id] AS [{nameof(UserDto.Id)}], " +
                         $"[User].[FullName] AS [{nameof(UserDto.FullName)}], " +
                         $"[User].[UserName] AS [{nameof(UserDto.UserName)}], " +
                         $"[User].[Email] AS [{nameof(UserDto.Email)}], " +
                         $"[User].[PhoneNumber] AS [{nameof(UserDto.PhoneNumber)}]," +
                         $"[User].[IsActive] AS [{nameof(UserDto.IsActive)}], " +
                         $"[Role].[Name] AS [{nameof(UserDto.Role)}] " +
                         "FROM [CMMS].[dbo].[AspNetUsers] AS [User] " +
                         "LEFT JOIN [CMMS].[dbo].[AspNetUserRoles] AS [UserRole] " +
                         "ON [UserRole].[UserId] = [User].[Id] " +
                         "LEFT JOIN [CMMS].[dbo].[AspNetRoles] AS [Role] " +
                         "ON [UserRole].[RoleId] = [Role].[Id] " +
                         "WHERE [User].[Id] = @Id AND [User].[IsActive] = 1";
            var user = await connection.QuerySingleOrDefaultAsync<UserDto>(sql, new { query.Id });

            if (user == null)
                throw new NotFoundException("User with the given id hasn't been found", null);

            return user;
        }
    }
}
