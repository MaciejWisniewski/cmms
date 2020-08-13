using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Emails;
using Dapper;
using MediatR;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class FailureRegisteredNotificationHandler : INotificationHandler<FailureRegisteredNotification>
    {
        private readonly EmailsSettings _emailSettings;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEmailSender _emailSender;

        public FailureRegisteredNotificationHandler(
            EmailsSettings emailsSettings, 
            ISqlConnectionFactory sqlConnectionFactory,
            IEmailSender emailSender)
        {
            _emailSettings = emailsSettings;
            _sqlConnectionFactory = sqlConnectionFactory;
            _emailSender = emailSender;
        }

        public async Task Handle(FailureRegisteredNotification notification, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT [Worker].[Email] " +
                                "FROM [CMMS].[maintenance].[Workers] AS [Worker] " +
                                "JOIN [CMMS].[maintenance].[ResourceAccesses] AS [Access] " +
                                "ON [Access].[WorkerId] = [Worker].[Id] " +
                                "WHERE [Access].[ResourceId] = @ResourceId";
            var workerEmails = await connection.QueryAsync<string>(sql,
                new
                {
                    ResourceId = notification.ResourceId.Value
                });

            var occurredOn = notification.FailureOccurredOn.ToString(new CultureInfo("pl-PL"));
            string subject = $"[Failure] {notification.ResourceName} {occurredOn}";
            string emailContent = $"The failure concerning resource: {notification.ResourceName} " +
                                  $"occured on {occurredOn}.  \n" +
                                  $"Description: {notification.ProblemDescription}";

            var tasks = workerEmails.Select(async email =>
            {
                var emailMessage = new EmailMessage(
                    _emailSettings.FromAddressEmail,
                    email,
                    subject,
                    emailContent
                    );

                await _emailSender.SendEmailAsync(emailMessage);
            });

            await Task.WhenAll(tasks);
        }
    }
}
