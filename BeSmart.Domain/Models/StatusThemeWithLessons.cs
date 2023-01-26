using BeSmart.Domain.DTOs.StatusLesson;

namespace BeSmart.Domain.Models
{
    public class StatusThemeWithLessons
    {
        public int Id { get; set; }
        public string? NameOfTheme { get; set; }
        public int? CountOfLessons { get; set; }
        public List<StatusLesson> StatusLessons { get; set; }
    }
}

