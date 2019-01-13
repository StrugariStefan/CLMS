using Gamification.API.Models;
using FluentValidation;

namespace Gamification.API.Validators
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            RuleFor(answer => answer.ActualAnswer).NotEmpty();
        }
    }
}
