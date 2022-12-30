using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Category : EntityBase
{
    public string Name { get; set; }
    public List<Course>? Courses { get; set; }
}