using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System;

namespace MyWebApp.Services.EmailSender
{
    public class MailKitEmailSender:IEmailSender
    {
        public string Key { get; set; }
        public string EmailMy { get; set; }

        public async Task SendMessage(string emailTo, string EmailMessage, string EmailBody)
        {
            //var myPassword = Key;
            //var emailMessage = new MimeMessage();

            //emailMessage.From.Add(new MailboxAddress("Администрация сайта", emailTo));
            //emailMessage.To.Add(new MailboxAddress("", emailTo));
            //emailMessage.Subject = EmailMessage;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //{
            //    Text = EmailBody
            //};

            //using (var client = new SmtpClient())
            //{
            //    await client.ConnectAsync("smtp.gmail.com", 25, true);
            //    await client.AuthenticateAsync(EmailMy, myPassword);
            //    await client.SendAsync(emailMessage);
            //    await client.DisconnectAsync(true);
            //}
        }
    }
}
