using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models
{
    public class StatusLesson : EntityBase
    {
        public string? Status { get; set; }
        public int StatusThemeId { get; set; }
        public StatusTheme StatusTheme { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
