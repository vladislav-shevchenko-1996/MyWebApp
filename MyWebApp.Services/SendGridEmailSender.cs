// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using MyWebApp.Services.EmailSender;


namespace MyWebApp.Services.EmailSender
{
    public class SendGridEmailSender:IEmailSender
    {
       
        public async Task SendMessage(string emailTo, string EmailMessage, string EmailBody)
        {
           
            var apiKey = Environment.GetEnvironmentVariable("KOTE");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(emailTo, "Example User");
            var subject = EmailMessage;
            var to = new EmailAddress(emailTo, "Example User");
            var plainTextContent = $"and easy to do anywhere, even with C# {EmailMessage}";
            var htmlContent = $"<strong>{EmailBody}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
