using CMMS.Application.Configuration.Emails;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            ;
            // Integration with email service.

            return;
        }
    }
}
