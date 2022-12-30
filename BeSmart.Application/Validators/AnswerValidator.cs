using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Validators
{
    public class AnswerValidator : AbstractValidator<Answer>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.Text).NotEmpty().MaximumLength(150);
            RuleFor(a => a.Fidelity).NotEmpty();
        }
    }
}