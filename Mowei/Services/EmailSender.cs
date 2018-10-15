using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using Mowei.Common;

namespace Mowei.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", CommEnvironment.SmtpAccount));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(CommEnvironment.SmtpHost, CommEnvironment.SmtpPort, false).ConfigureAwait(false);
                client.Authenticate(CommEnvironment.SmtpAccount, CommEnvironment.SmtpPassWord);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }

        }
    }
}
