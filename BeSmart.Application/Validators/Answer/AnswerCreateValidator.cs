using BeSmart.Domain.DTOs.Answer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Validators.Answer
{
    public class AnswerCreateValidator : AbstractValidator<AnswerCreationDTO>
    {
        public AnswerCreateValidator()
        {
            RuleFor(a => a.Text).Length(3, 150);
            RuleFor(a => a.Fidelity).NotEmpty();
        }   
    }
}
