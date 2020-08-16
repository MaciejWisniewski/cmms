using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.Emails;
using MediatR;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.SendFailureRegisteredEmail
{
    internal class SendFailureRegisteredEmailCommandHandler : ICommandHandler<SendFailureRegisteredEmailCommand>
    {
        private readonly IEmailSender _emailSender;
        private readonly EmailsSettings _emailsSettings;

        internal SendFailureRegisteredEmailCommandHandler(IEmailSender emailSender, EmailsSettings emailsSettings)
        {
            _emailSender = emailSender;
            _emailsSettings = emailsSettings;
        }

        public async Task<Unit> Handle(SendFailureRegisteredEmailCommand command, CancellationToken cancellationToken)
        {
            var occurredOn = command.FailureOccurredOn.ToString(new CultureInfo("pl-PL"));
            string subject = $"[Failure] {command.ResourceName} {occurredOn}";
            string messageContent = $"The failure concerning resource: {command.ResourceName} " +
                                    $"occured on {occurredOn}.  \n" +
                                    $"Description: {command.ProblemDescription}";

            var emailMessage = new EmailMessage(
                    _emailsSettings.FromEmailAddress,
                    command.ToEmailAddress,
                    subject,
                    messageContent
                    );

            await _emailSender.SendEmailAsync(emailMessage);

            return Unit.Value;
        }
    }
}
