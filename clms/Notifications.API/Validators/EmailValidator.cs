using FluentValidation;
using Notifications.API.Models;

namespace Notifications.API.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(email => email).NotNull();
            RuleFor(email => email.To).NotEmpty();
            RuleForEach(email => email.To).Matches(@"^[a-zA-Z0-9_.-]+@[a-z.]+.[a-z]+$");
            RuleFor(email => email.Subject).NotEmpty();
            RuleFor(email => email.Body).NotEmpty();
        }
    }
}