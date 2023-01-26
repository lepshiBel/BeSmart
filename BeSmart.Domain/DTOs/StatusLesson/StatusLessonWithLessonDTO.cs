using BeSmart.Domain.DTOs.Lesson;

namespace BeSmart.Domain.DTOs.StatusLesson
{
    public class StatusLessonWithLessonDTO
    {
        public int Id { get; set; }
        public string? StatusLesson { get; set; }
        public LessonDTO Lesson { get; set; }
    }
}
