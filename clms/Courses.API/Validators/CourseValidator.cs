using Courses.API.Models;
using FluentValidation;

namespace Courses.API.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(course => course.Name).NotEmpty().Matches("[A-Z][a-z'-]+[ ][A-Z][.][ ][A-Z][a-z'-]");
            RuleFor(course => course.Description).NotEmpty().MaximumLength(200);
        }
    }
}
