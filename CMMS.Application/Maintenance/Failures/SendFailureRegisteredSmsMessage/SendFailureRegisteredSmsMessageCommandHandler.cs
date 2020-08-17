using CMMS.Application.Configuration.Commands;
using CMMS.Application.Configuration.SmsMessages;
using MediatR;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CMMS.Application.Maintenance.Failures.SendFailureRegisteredSmsMessage
{
    internal class SendFailureRegisteredSmsMessageCommandHandler : ICommandHandler<SendFailureRegisteredSmsMessageCommand>
    {
        private readonly ISmsMessageSender _smsMessageSender;
        private readonly SmsMessagesSettings _smsMessagesSettings;

        public SendFailureRegisteredSmsMessageCommandHandler(ISmsMessageSender smsMessageSender, SmsMessagesSettings smsMessagesSettings)
        {
            _smsMessageSender = smsMessageSender;
            _smsMessagesSettings = smsMessagesSettings;
        }

        public async Task<Unit> Handle(SendFailureRegisteredSmsMessageCommand command, CancellationToken cancellationToken)
        {
            var occurredOn = command.FailureOccurredOn.ToString(new CultureInfo("pl-PL"));
            string messageContent = $"[Failure] Resource: {command.ResourceName} on {occurredOn}. " +
                             $"{command.ProblemDescription}";

            var smsMessage = new SmsMessage(
                _smsMessagesSettings.FromPhoneNumber,
                command.ToPhoneNumber,
                messageContent);

            await _smsMessageSender.SendSmsMessageAsync(smsMessage);

            return Unit.Value;
        }
    }
}
