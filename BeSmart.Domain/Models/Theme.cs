using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Theme : EntityBase
{
    public string Name { get; set; }

    public int CountLesson { get; set; }

    public int CountTest { get; set; }

    public int CourseId { get; set; }

    public Course Course { get; set; }

    public List<Lesson> Lessons { get; set; }
    public List<Test> Tests { get; set; }
    public List<StatusTheme> StatusThemes { get; set; }
}