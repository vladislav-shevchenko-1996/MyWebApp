using FluentValidation;

namespace MyWebApp.Models
{
    public class SendEmailMessageVm
    {
        public string EmailTo { get; set; }
        public string EmailMessage { get; set; }
        public string EmailBody { get; set; }
    }
    public class SendEmailMessageVmValidator:AbstractValidator<SendEmailMessageVm>
    {
        public SendEmailMessageVmValidator()
        {
            RuleFor(x => x.EmailTo).NotNull().EmailAddress();
            RuleFor(x => x.EmailMessage).MinimumLength(3);
        }
    }
}
