using BeSmart.Domain.Models;
using FluentValidation;

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
