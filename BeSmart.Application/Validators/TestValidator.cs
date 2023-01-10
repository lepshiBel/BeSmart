using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Validators
{
    public class TestValidator : AbstractValidator<Test>
    {
        public TestValidator()
        {
            RuleFor(l => l.Name).NotEmpty().Length(3, 150);
            RuleFor(l => l.QuestionsCount).NotEmpty();
            RuleFor(c => c.ThemeId).NotNull();
            RuleFor(c => c.Theme).SetValidator(new ThemeValidator());
        }
    }
}
