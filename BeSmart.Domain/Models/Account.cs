using BeSmart.Domain.Models.Base;

namespace BeSmart.Domain.Models;

public class Account : EntityBase
{
    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string UserPassword { get; set; }

    public List<Course> CreatedCourses { get; set; }
}