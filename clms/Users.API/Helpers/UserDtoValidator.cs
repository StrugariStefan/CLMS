using FluentValidation;
using Users.API.Models;

namespace Users.API.Helpers
{
//    https://cecilphillip.com/fluent-validation-rules-with-asp-net-core/
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Name).NotNull().Matches("[A-Z][a-z'-]+[ ][A-Z.][ ][A-Z][a-z'-]");
            RuleFor(user => user.Phone).NotNull().Matches("(00|\\+)40\\d{10}");
            RuleFor(user => user.Email).NotNull().Matches("[a-zA-Z0-9_.-]+@[a-z]+.[a-z]+");
        }
    }
}
