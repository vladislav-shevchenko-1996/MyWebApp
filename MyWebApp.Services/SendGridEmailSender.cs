// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using MyWebApp.Services.EmailSender;
using MyWebApp.Common;


namespace MyWebApp.Services.EmailSender
{
    public class SendGridEmailSender:IEmailSender
    {
        public string Key { get; set; }
        public string EmailMy { get; set; }
        public async Task SendMessage(string email, string EmailMessage, string EmailBody)
        {
            var client = new SendGridClient(Key);
            var from = new EmailAddress(EmailMy, "Example User");
            var subject = EmailMessage;
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = $"and easy to do anywhere, even with C# {EmailMessage}";
            var htmlContent = $"<strong>{EmailBody}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
