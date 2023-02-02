using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Lesson : EntityBase
{
    public string Name { get; set; }

    public string Text { get; set; }

    public int ThemeId { get; set; }

    public Theme Theme { get; set; }

    public List<Card> Cards { get; set; }
    public List<StatusLesson> StatusLessons { get; set; }
}