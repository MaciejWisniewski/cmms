using System.Threading.Tasks;

namespace CMMS.Application.Configuration.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
