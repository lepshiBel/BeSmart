namespace BeSmart.Persistence.Models;

public class Theme
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int CountLesson { get; set; }

    public int CountTest { get; set; }

    public int CourseId { get; set; }

    public Course Course { get; set; }

    public ICollection<Lesson> Lessons { get; set; }
}