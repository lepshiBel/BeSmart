using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Validators
{
    public class LessonValidator : AbstractValidator<Lesson>
    {
        public LessonValidator()
        {
            RuleFor(l => l.Name).NotEmpty().Length(3, 150);
            RuleFor(l => l.Text).Length(3, 255);
            RuleFor(c => c.ThemeId).NotNull();
            RuleFor(c => c.Theme).SetValidator(new ThemeValidator());
        }   
    }
}
