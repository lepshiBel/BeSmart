using BeSmart.Domain.Models;
using FluentValidation;

namespace BeSmart.Application.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c=>c.Name).NotEmpty().Length(3, 200);
        }
    }
}
