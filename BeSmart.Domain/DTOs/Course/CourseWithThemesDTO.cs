using BeSmart.Domain.DTOs.Theme;

namespace BeSmart.Domain.DTOs.Course
{
    public class CourseWithThemesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int CreatedById { get; set; }
        public int CountThemes { get; set; }
        public ICollection<ThemeDTO> Themes { get; set; }
    }
}
