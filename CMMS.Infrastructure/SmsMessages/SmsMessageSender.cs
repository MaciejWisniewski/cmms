using CMMS.Application.Configuration.SmsMessages;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.SmsMessages
{
    public class SmsMessageSender : ISmsMessageSender
    {
        public Task SendSmsMessageAsync(SmsMessage smsMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
