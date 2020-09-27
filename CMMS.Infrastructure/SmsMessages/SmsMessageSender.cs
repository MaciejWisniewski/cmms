using CMMS.Application.Configuration.SmsMessages;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CMMS.Infrastructure.SmsMessages
{
    public class SmsMessageSender : ISmsMessageSender
    {
        public Task SendSmsMessageAsync(SmsMessage smsMessage)
        {
            const string accountSid = "ACecfe7f3d83f476addc55b2d8ecc080f7";
            const string authToken = "dafae66102a798130fa11bbe53276678";

            TwilioClient.Init(accountSid, authToken);
            MessageResource.Create(
                body: smsMessage.Content,
                from: new Twilio.Types.PhoneNumber(smsMessage.From), 
                to: new Twilio.Types.PhoneNumber(smsMessage.To)
            );
            
            return Task.CompletedTask;
        }
    }
}
