using Gamification.API.Models;
using FluentValidation;

namespace Gamification.API.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(question => question.ActualQuestion).NotEmpty();
            RuleFor(question => question.LevelOfInterest).NotEmpty().IsInEnum<Question, LevelOfInterest>();
            RuleFor(question => question.Type).NotEmpty().IsInEnum<Question, Type>();
        }
    }
}
