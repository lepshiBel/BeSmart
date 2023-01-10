namespace BeSmart.Domain.DTOs.Course
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int CountThemes { get; set; }
        public int CreatedById { get; set; }
    }
}
