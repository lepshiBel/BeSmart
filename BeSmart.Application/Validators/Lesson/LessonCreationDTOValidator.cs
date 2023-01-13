using BeSmart.Domain.DTOs.Lesson;
using FluentValidation;

namespace BeSmart.Application.Validators.Lesson
{
    public class LessonCreationDTOValidator : AbstractValidator<LessonCreationDTO>
    {
        public LessonCreationDTOValidator() 
        {
            RuleFor(l => l.Name).NotEmpty().Length(3, 150);
            RuleFor(l => l.Text).Length(3, 255);
        }
    }
}
