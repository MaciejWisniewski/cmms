using CMMS.Application.Configuration.Emails;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CMMS.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(EmailMessage message)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            mailMessage.From = new MailAddress(message.From);
            mailMessage.To.Add(new MailAddress(message.To));
            mailMessage.Subject = message.Subject;
            //mailMessage.IsBodyHtml = true; //to make mailMessage body as html  
            mailMessage.Body = message.Content;

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(message.From, "Haslo1!!");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mailMessage);

            await Task.CompletedTask;
        }
    }
}
