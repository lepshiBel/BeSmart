using BeSmart.Domain.DTOs;
using FluentValidation;

namespace BeSmart.Application.Validators.Test
{
    public class TestCreationDTOValidator : AbstractValidator<TestCreationDTO>
    {
        public TestCreationDTOValidator() 
        {
            RuleFor(l => l.Name).NotEmpty().Length(3, 150);
            RuleFor(l => l.QuestionsCount).NotEmpty();
        }
    }
}
