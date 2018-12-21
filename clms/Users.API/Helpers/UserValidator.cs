using FluentValidation;
using Users.API.Models;

namespace Users.API.Helpers
{
    public class UserValidator : AbstractValidator<UserCreateDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name).NotNull().Matches("[A-Z][a-z'-]+[ ][A-Z][.][ ][A-Z][a-z'-]");
            RuleFor(user => user.Phone).NotNull().Matches("\\d{10}");
            RuleFor(user => user.Email).NotNull().Matches("[a-zA-Z0-9_.-]+@[a-z]+.[a-z]+");
            RuleFor(user => user.Password).NotNull().Matches("^.{6,}$");
            RuleFor(user => user.Role).NotNull().InclusiveBetween(1, 2);
        }
    }
}