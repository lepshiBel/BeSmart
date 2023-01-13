using BeSmart.Domain.DTOs.Question;
using FluentValidation;

namespace BeSmart.Application.Validators.Question
{
    public class QuestionCreationDTOValidator : AbstractValidator<QuestionCreationDTO>
    {
        public QuestionCreationDTOValidator() 
        {
            RuleFor(q => q.Text).NotEmpty().Length(3, 150);
        }
    }
}
