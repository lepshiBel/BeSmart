using BeSmart.Domain.DTOs.Test;
using FluentValidation;

namespace BeSmart.Application.Validators.Test
{
    public class TestUpdateDTOValidator : AbstractValidator<TestUpdateDTO>
    {
        public TestUpdateDTOValidator() 
        {
            RuleFor(l => l.Name).NotEmpty().Length(3, 150);
            RuleFor(l => l.Countquestion).NotEmpty();
        } 
    }
}
