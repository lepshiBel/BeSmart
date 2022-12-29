using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Course : EntityBase
{
    public string Name { get; set; }

    public ICollection<Theme> CourseThemes { get; set; } // Todo: Уточнить про CourseTheme

    public int CreatedById { get; set; }

    public Account CreatedBy { get; set; }
}