using System.Threading.Tasks;

namespace MyWebApp.Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendMessage(string emailTo, string emailMessge, string emailBody);
    }
}
