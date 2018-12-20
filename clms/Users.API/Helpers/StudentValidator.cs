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
            const string studyProgram = "(bachelor)|(master)";
            const string group = "[A][1-5][A-Z][1-9]";

            RuleFor(student => student.StudyProgram).NotNull().Matches(studyProgram);
            RuleFor(student => student.Year).NotNull().InclusiveBetween(1, 5);
            RuleFor(student => student.Group).NotNull().Matches(group);
        }
    }
}