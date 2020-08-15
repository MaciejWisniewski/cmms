using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Processing;
using CMMS.Application.Maintenance.Failures.SendFailureRegisteredEmail;
using CMMS.Application.Maintenance.Failures.SendFailureRegisteredSmsMessage;
using Dapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class FailureRegisteredNotificationHandler : INotificationHandler<FailureRegisteredNotification>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ICommandsScheduler _commandsScheduler;

        public FailureRegisteredNotificationHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            ICommandsScheduler commandsScheduler)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(FailureRegisteredNotification notification, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[Worker].[Email] AS [{nameof(WorkerDto.Email)}], " +
                         $"[Worker].[PhoneNumber] AS [{nameof(WorkerDto.PhoneNumber)}] " +
                         "FROM [CMMS].[maintenance].[Workers] AS [Worker] " +
                         "JOIN [CMMS].[maintenance].[ResourceAccesses] AS [Access] " +
                         "ON [Access].[WorkerId] = [Worker].[Id] " +
                         "WHERE [Access].[ResourceId] = @ResourceId";
            var workers = await connection.QueryAsync<WorkerDto>(sql,
                new
                {
                    ResourceId = notification.ResourceId.Value
                });

            foreach (var worker in workers)
            {
                await _commandsScheduler.EnqueueAsync(new SendFailureRegisteredSmsMessageCommand(
                    Guid.NewGuid(),
                    notification.ResourceName,
                    notification.FailureOccurredOn,
                    notification.ProblemDescription,
                    worker.PhoneNumber));

                await _commandsScheduler.EnqueueAsync(new SendFailureRegisteredEmailCommand(
                    Guid.NewGuid(),
                    notification.ResourceName,
                    notification.FailureOccurredOn,
                    notification.ProblemDescription,
                    worker.Email));
            }
        }
    }
}
