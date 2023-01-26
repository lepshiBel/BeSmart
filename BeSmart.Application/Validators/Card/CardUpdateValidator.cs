using BeSmart.Domain.DTOs.Card;
using FluentValidation;

namespace BeSmart.Application.Validators.Card
{
    public class CardUpdateValidator : AbstractValidator<CardUpdateDTO>
    {
        public CardUpdateValidator()
        {
            RuleFor(c => c.Word).Length(3, 150);
            RuleFor(c => c.Text).Length(3, 255);
            RuleFor(c => c.Transctipt).Length(3, 150);
            RuleFor(c => c.ImageUrl).Length(3, 255);
        }
    }
}
