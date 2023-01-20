using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Test : EntityBase
{
    public string Name { get; set; }

    public int QuestionsCount { get; set; }

    public int ThemeId { get; set; }

    public Theme Theme { get; set; }

    public List<Question> Questsions { get; set; }
    public List<StatusTest> StatusTests { get; set; }
}