using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.API.Models;
using FluentValidation;

namespace Courses.API.Validators
{
    public class ResourceFileValidator : AbstractValidator<ResourceFile>
    {
        public ResourceFileValidator()
        {
            RuleFor(resourceFile => resourceFile.Name).NotEmpty();
            RuleFor(resourceFile => resourceFile.Description).NotEmpty().MaximumLength(200);
            RuleFor(resourceFile => resourceFile.Type).NotEmpty().IsInEnum<ResourceFile, ResourceType>();
        }
    }
}
