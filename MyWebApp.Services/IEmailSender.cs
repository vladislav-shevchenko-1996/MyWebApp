using System.Threading.Tasks;

namespace MyWebApp.Services.EmailSender
{
    public interface IEmailSender
    {
        public string Key { get; set; }
        Task SendMessage(string emailTo, string emailMessge, string emailBody);
    }
}
