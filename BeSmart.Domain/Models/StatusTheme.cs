using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models
{
    public class StatusTheme : EntityBase
    {
        public string? Status { get; set; }
        public int? AmountOfCompletedLessons { get; set; }
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }
        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
        public List<StatusLesson> StatusLessons { get; set; }
        public List<StatusTest> StatusTests { get; set; }
    }
}
