using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models
{
    public class StatusTest : EntityBase
    {
        public string? Status { get; set; }
        public int? Mark { get; set; }
        public int? AmountOfFaithfullAnswers { get; set; }
        public int? AmountOfIncorrectAnswers { get; set; }
        public int StatusThemeId { get; set; }
        public StatusTheme StatusTheme { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
