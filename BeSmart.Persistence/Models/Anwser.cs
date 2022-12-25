namespace BeSmart.Persistence.Models;

public class Anwser
{
    public int Id { get; set; }

    public string Text { get; set; }

    public bool Fidelty { get; set; }

    public int QuestionId { get; set; }

    public Questsion Question { get; set; }
}