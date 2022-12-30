using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Validators
{
    public class QuestionValidator : AbstractValidator<Question>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Text).NotEmpty().Length(3, 150);
        }
    }
}
