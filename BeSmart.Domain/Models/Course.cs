using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Course : EntityBase
{
    public string Name { get; set; }

    public int CountOfThemes { get; set; }
    public int CreatedById { get; set; }

    public Account CreatedBy { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; }
    public List<Theme> CourseThemes { get; set; }

}