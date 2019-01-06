using Courses.API.Models;
using FluentValidation;

namespace Courses.API.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(course => course.Name).NotEmpty();
            RuleFor(course => course.Description).NotEmpty().MaximumLength(200);
        }
    }
}
