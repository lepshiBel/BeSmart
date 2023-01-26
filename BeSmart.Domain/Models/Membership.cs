using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models
{
    public class Membership : EntityBase
    {
        public string? Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public List<StatusTheme> StatusThemes { get; set; }
    }
}
