using CMMS.Application.Configuration.Data;
using CMMS.Application.Configuration.Emails;
using CMMS.Application.Configuration.SmsMessages;
using Dapper;
using MediatR;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.RegisterFailure
{
    public class FailureRegisteredNotificationHandler : INotificationHandler<FailureRegisteredNotification>
    {
        private readonly EmailsSettings _emailsSettings;
        private readonly SmsMessagesSettings _smsMessagesSettings;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEmailSender _emailSender;
        private readonly ISmsMessageSender _smsMessageSender;

        public FailureRegisteredNotificationHandler(
            EmailsSettings emailsSettings, 
            SmsMessagesSettings smsMessagesSettings,
            ISqlConnectionFactory sqlConnectionFactory,
            IEmailSender emailSender,
            ISmsMessageSender smsMessageSender)
        {
            _emailsSettings = emailsSettings;
            _smsMessagesSettings = smsMessagesSettings;
            _sqlConnectionFactory = sqlConnectionFactory;
            _emailSender = emailSender;
            _smsMessageSender = smsMessageSender;
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
            string messageContent = $"The failure concerning resource: {notification.ResourceName} " +
                                    $"occured on {occurredOn}.  \n" +
                                    $"Description: {notification.ProblemDescription}";

            var tasks = workerEmails.Select(async email =>
            {
                var emailMessage = new EmailMessage(
                    _emailsSettings.FromAddressEmail,
                    email,
                    subject,
                    messageContent
                    );

                var smsMessage = new SmsMessage(
                    _smsMessagesSettings.FromPhoneNumber,
                    "1234321234", //TODO: Put worker number here
                    messageContent
                    );

                await _smsMessageSender.SendSmsMessageAsync(smsMessage);
                await _emailSender.SendEmailAsync(emailMessage);
            });

            await Task.WhenAll(tasks);
        }
    }
}
