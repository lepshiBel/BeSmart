namespace BeSmart.Persistence.Models;

public class Test
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int QuestionsCount { get; set; }

    public int ThemeId { get; set; }

    public Theme Theme { get; set; }

    public ICollection<Questsion> Questsions { get; set; }
}