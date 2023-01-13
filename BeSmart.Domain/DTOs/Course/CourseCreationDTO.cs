namespace BeSmart.Domain.DTOs.Course
{
    public class CourseCreationDTO
    {
        public string Name { get; set; }
        public int CountOfThemes { get; set; }
        public int CreatedById { get; set; }
        public int CategoryId { get; set; }
    }
}
