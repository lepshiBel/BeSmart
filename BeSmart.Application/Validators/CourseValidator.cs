using BeSmart.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Validators
{
    public class CourseValidator : AbstractValidator<Course>
    {
        public CourseValidator()
        {
            RuleFor(c => c.Name).NotEmpty().Length(3, 150);
            RuleFor(c => c.CountOfThemes).NotEmpty();
            RuleFor(c => c.CreatedById).NotNull();
            RuleFor(c => c.CategoryId).NotNull();
            RuleFor(c => c.CreatedBy).SetValidator(new AccountValidator());
            RuleFor(c => c.Category).SetValidator(new CategoryValidator());
        }
    }
}
