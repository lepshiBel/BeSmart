namespace BeSmart.Persistence.Models;

public class Questsion
{
    public int Id { get; set; }

    public string Text { get; set; }

    public int TestId { get; set; }

    public Test Test { get; set; }
}