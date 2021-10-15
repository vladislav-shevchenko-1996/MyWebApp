using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace MyWebApp.Services.EmailSender
{
    public class MailKitEmailSender:IEmailSender
    {
        public async Task SendMessage(string emailTo, string EmailMessage, string EmailBody)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "vladisla1996@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", emailTo));
            emailMessage.Subject = EmailMessage;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = EmailBody
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.sendgrid.net", 465, false);
                await client.AuthenticateAsync("apikey", "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");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
