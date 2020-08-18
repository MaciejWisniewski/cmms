using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Data;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand, Unit>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ProcessInternalCommandsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            string sql = "SELECT " +
                         $"[Command].[Id] AS [{nameof(InternalCommandDto.Id)}], " +
                         $"[Command].[Type] AS [{nameof(InternalCommandDto.Type)}], " +
                         $"[Command].[Data] AS [{nameof(InternalCommandDto.Data)}] " +
                         "FROM [CMMS].[app].[InternalCommands] AS [Command] " +
                         "WHERE [Command].[ProcessedDate] IS NULL";
            var commands = await connection.QueryAsync<InternalCommandDto>(sql);

            var internalCommandsList = commands.AsList();

            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });

            foreach (var internalCommand in internalCommandsList)
            {
                var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(
                    internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                {
                    await connection.ExecuteScalarAsync("UPDATE [CMMS].[app].[InternalCommands] " +
                                                        "SET ProcessedDate = @NowDate, " +
                                                        "Error = @Error " +
                                                        "WHERE [Id] = @Id",
                        new
                        {
                            NowDate = DateTime.UtcNow,
                            Error = result.FinalException.ToString(),
                            internalCommand.Id
                        });
                }
                else
                {
                    const string sqlUpdateProcessedDate = "UPDATE [CMMS].[app].[InternalCommands] " +
                                    "SET [ProcessedDate] = @Date " +
                                    "WHERE [Id] = @Id";

                    await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                    {
                        Date = DateTime.UtcNow,
                        internalCommand.Id
                    });
                }
            }

            return Unit.Value;
        }

        private async Task ProcessCommand(InternalCommandDto internalCommand)
        {
            Type type = Assemblies.Application.GetType(internalCommand.Type);
            dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }

        private class InternalCommandDto
        {
            public Guid Id { get; set; }

            public string Type { get; set; }

            public string Data { get; set; }
        }
    }
}
