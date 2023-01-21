using BeSmart.Domain.DTOs.Course;
using FluentValidation;

namespace BeSmart.Application.Validators.Course
{
    public class CourseCreateValidator : AbstractValidator<CourseCreationDTO>
    {
        public CourseCreateValidator()
        {
            RuleFor(c => c.Name).Length(3, 150);
            RuleFor(c => c.CountOfThemes).NotEmpty();
        }
    }
}
