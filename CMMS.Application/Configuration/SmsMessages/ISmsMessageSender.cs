using System.Threading.Tasks;

namespace CMMS.Application.Configuration.SmsMessages
{
    public interface ISmsMessageSender
    {
        Task SendSmsMessageAsync(SmsMessage smsMessage);
    }
}
