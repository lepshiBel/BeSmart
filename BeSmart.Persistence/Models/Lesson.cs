namespace BeSmart.Persistence.Models;

public class Lesson
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Text { get; set; }

    public int ThemeId { get; set; }

    public Theme Theme { get; set; }

    public ICollection<Card> Cards { get; set; }
}