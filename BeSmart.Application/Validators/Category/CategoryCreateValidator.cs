using BeSmart.Domain.DTOs.Category;
using FluentValidation;

namespace BeSmart.Application.Validators.Category
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreationDTO>
    {
        public CategoryCreateValidator()
        {
            RuleFor(a=>a.Text).Length(3, 200);
        }
    }
}
