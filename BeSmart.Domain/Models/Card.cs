using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Card : EntityBase
{
    public string Word { get; set; }

    public string? Text { get; set; }

    public string? ImageUrl { get; set; }

    public string? Transctipt { get; set; }

    public int LessonId { get; set; }

    public Lesson Lesson { get; set; }
}