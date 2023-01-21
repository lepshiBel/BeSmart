using BeSmart.Domain.DTOs.Theme;
using FluentValidation;

namespace BeSmart.Application.Validators.Theme
{
    public class ThemeCreationDTOValidator : AbstractValidator<ThemeCreationDTO>
    {
        public ThemeCreationDTOValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 150);
            RuleFor(x => x.CountLesson).NotEmpty();
            RuleFor(x => x.CountTest).NotEmpty();
        }
    }
}
