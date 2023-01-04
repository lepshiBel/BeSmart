using BeSmart.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Application.Validators
{
    public class CardValidator : AbstractValidator<Card>
    {
        public CardValidator()
        {
            RuleFor(c => c.Word).NotEmpty().Length(3, 150);
            RuleFor(c => c.Text).Length(3, 255);
            RuleFor(c => c.Transctipt).Length(3, 150);
            RuleFor(c => c.ImageUrl).Length(3, 255);
            RuleFor(c => c.LessonId).NotNull();
            RuleFor(c => c.Lesson).SetValidator(new LessonValidator());
        }
    }
}
