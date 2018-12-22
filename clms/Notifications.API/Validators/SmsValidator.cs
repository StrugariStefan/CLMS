using FluentValidation;
using Notifications.API.Models;

namespace Notifications.API.Validators
{
    public class SmsValidator : AbstractValidator<Sms>
    {
        public SmsValidator()
        {
            RuleFor(sms => sms).NotNull();
            RuleFor(sms => sms.To).NotEmpty();
            RuleForEach(sms => sms.To).Matches(@"^\+\d{11}$");
            RuleFor(sms => sms.Body).NotEmpty();
        }
    }
}