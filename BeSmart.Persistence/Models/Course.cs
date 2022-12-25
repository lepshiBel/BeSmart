namespace BeSmart.Persistence.Models;

public class Course
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Theme> CourseThemes { get; set; } // Todo: Уточнить про CourseTheme

    public int CreatedById { get; set; }

    public Account CreatedBy { get; set; }
}