using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Text).NotEmpty().Length(3, 150);
            RuleFor(q => q.TestId).NotNull();
            RuleFor(q => q.Test).SetValidator(new TestValidator());
        }
    }
}
