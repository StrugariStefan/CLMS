using System;
using System.Collections.Generic;
using FluentValidation;
using Users.API.Models;

namespace Users.API.Helpers
{
    public class StudentValidator : AbstractValidator<StudentDto>
    {
        public StudentValidator()
        {
            List<int> yearValues = new List<int>() {1, 2, 3, 4, 5};
            const string studyProgram = "(bachelor)|(master)";
            const string group = "[A][1-5][A-Z][1-9]";

            RuleFor(student => student.StudyProgram)
                .NotNull()
                .Matches(studyProgram);

            RuleFor(student => student.Year)
                .NotNull()
                .Must(student => yearValues.Contains(student))
                .WithMessage(errorMessage: "Please only use: " + String.Join(",", yearValues));

            RuleFor(student => student.Group)
                .NotNull()
                .Matches(group);
        }
    }
}