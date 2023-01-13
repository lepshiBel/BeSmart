using BeSmart.Domain.DTOs.Course;
using FluentValidation;

namespace BeSmart.Application.Validators.Course
{
    public class CourseUpdateValidator : AbstractValidator<CourseUpdateDTO>
    {
        public CourseUpdateValidator()
        {
            RuleFor(c => c.Name).Length(3, 150);
            RuleFor(c => c.CountOfThemes).NotEmpty();
        }
    }
}
