using BeSmart.Domain.DTOs.Lesson;

namespace BeSmart.Domain.DTOs.Theme
{
    public class ThemeWithLessonsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountLesson { get; set; }

        public int CountTest { get; set; }
        public ICollection<LessonDTO> Lessons { get; set; }
    }
}
