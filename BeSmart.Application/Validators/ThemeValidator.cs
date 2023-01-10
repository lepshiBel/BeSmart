using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Validators
{
    public class ThemeValidator : AbstractValidator<Theme>
    {
        public ThemeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 150);
            RuleFor(x => x.CountLesson).NotEmpty();
            RuleFor(x => x.CountTest).NotEmpty();
            RuleFor(x => x.CourseId).NotNull();
            RuleFor(x => x.Course).SetValidator(new CourseValidator());


        }
    }
}
